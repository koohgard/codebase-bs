using System.Net;
using System.Text;
using System.Text.Json;
using Abstraction.Command;
using Abstraction.Command.Order.GetOrders;
using Application;

namespace Tests;
[TestFixture]
public class OrderTests
{
    private HttpClient client;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {

    }

    [SetUp]
    public void Setup()
    {
        var testFactory = new TestAppFactoy();
        client = testFactory.CreateClient();
        testFactory.ResetDatabase();
    }

    [TearDown]
    public void TearDown()
    {
        client.Dispose();
    }

    #region Create
    [Test]
    public async Task Order_Create_Exists()
    {
        // Act
        var newOrder = new { };
        var content = new StringContent(JsonSerializer.Serialize(newOrder), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/order", content);

        // Assert
        Assert.That(response.StatusCode, Is.Not.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task Order_Create_ReturnsValidationError()
    {
        var createBookResult = await TestUtils.CreateBook(client);
        // Arrange
        var newOrder = new
        {
            BookId = createBookResult.Id,
            Count = 0
        };
        var content = new StringContent(JsonSerializer.Serialize(newOrder), Encoding.UTF8, "application/json");
        // Act
        var response = await client.PostAsync("/api/order", content);
        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Order_Create_Unauthorized()
    {
        // Act        
        // Arrange
        var newOrder = new
        {
            BookId = 1,
            Count = 2
        };
        var content = new StringContent(JsonSerializer.Serialize(newOrder), Encoding.UTF8, "application/json");
        var response = await client.PatchAsync("/api/order", content);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.MethodNotAllowed));
    }

    [Test]
    public async Task Order_Create()
    {
        // Arrange
        var createBookResult = await TestUtils.CreateBook(client);
        var newOrder = new
        {
            BookId = createBookResult.Id,
            Count = 1
        };
        var content = new StringContent(JsonSerializer.Serialize(newOrder), Encoding.UTF8, "application/json");
        // Act
        var response = await client.PostAsync("/api/order", content);
        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }





    #endregion  
    #region GetOrders
    [Test]
    public async Task GetOrders_Exists()
    {
        // Act        
        var response = await client.GetAsync($"/api/order?pageIndex={1}&pageSize={10}");

        // Assert
        Assert.That(response.StatusCode, Is.Not.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GetOrders_GetOrders()
    {

        // Act        
        var createOrderResult = await TestUtils.CreateOrder(client);

        var response = await client.GetAsync($"/api/order?pageIndex={1}&pageSize={10}&startDate={DateTime.UtcNow.AddDays(-1)}&endDate={DateTime.UtcNow.AddDays(1)}");

        // Assert        
        response.EnsureSuccessStatusCode();
        var contentString = await response.Content.ReadAsStringAsync();
        var orders = JsonSerializer.Deserialize<PagingResult<GetOrdersQueryResult>>(contentString, TestUtils.JsonOptions);

        Assert.IsNotNull(orders);
        Assert.IsNotEmpty(orders.Data);
    }

    #endregion
}
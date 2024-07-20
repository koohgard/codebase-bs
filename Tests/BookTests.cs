using System.Net;
using System.Text;
using System.Text.Json;
using Abstraction.Command;
using Abstraction.Command.Book.GetBooks;

namespace Tests;
[TestFixture]
public class BookTests
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
    }

    [TearDown]
    public void TearDown()
    {
        client.Dispose();
    }

    #region Create
    [Test]
    public async Task Book_Create_Exists()
    {
        // Act
        var newBook = new { };
        var content = new StringContent(JsonSerializer.Serialize(newBook), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/book", content);

        // Assert
        Assert.That(response.StatusCode, Is.Not.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task Book_Create_ReturnsValidationError()
    {
        //TODO login with admin 
        // Arrange
        var newBook = new
        {
            Title = "BookTitle",
            Description = "Description",
            Price = 0,
            Stock = 0,
        };
        var content = new StringContent(JsonSerializer.Serialize(newBook), Encoding.UTF8, "application/json");
        // Act
        var response = await client.PostAsync("/api/book", content);
        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Book_Create_Unauthorized()
    {
        // Act
        var UpdateBook = new
        {
            Id = 1,
            Stock = 20
        };
        var content = new StringContent(JsonSerializer.Serialize(UpdateBook), Encoding.UTF8, "application/json");
        var response = await client.PatchAsync("/api/book", content);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Book_Create()
    {
        //TODO login with admin 
        // Arrange
        var newBook = new
        {
            Title = "BookTitle",
            Description = "Description",
            Price = 100,
            Stock = 100,
        };
        var content = new StringContent(JsonSerializer.Serialize(newBook), Encoding.UTF8, "application/json");
        // Act
        var response = await client.PostAsync("/api/book", content);
        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }





    #endregion
    #region UpdateStock
    [Test]
    public async Task Book_UpdateStock_Exists()
    {
        // Act
        var book = new { };
        var content = new StringContent(JsonSerializer.Serialize(book), Encoding.UTF8, "application/json");
        var response = await client.PatchAsync("/api/book", content);

        // Assert
        Assert.That(response.StatusCode, Is.Not.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task Book_UpdateStock_ValidationError()
    {
        //TODO login with admin 
        // Act
        var UpdateBook = new
        {
            Id = 0,
            Stock = -1
        };
        var content = new StringContent(JsonSerializer.Serialize(UpdateBook), Encoding.UTF8, "application/json");
        var response = await client.PatchAsync("/api/book", content);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Book_UpdateStock_Unauthorized()
    {
        // Act
        var UpdateBook = new
        {
            Id = 1,
            Stock = 20
        };
        var content = new StringContent(JsonSerializer.Serialize(UpdateBook), Encoding.UTF8, "application/json");
        var response = await client.PatchAsync("/api/book", content);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }


    [Test]
    public async Task Book_UpdateStock()
    {
        var createBookResult = await TestUtils.CreateBook(client);
        // Act
        var updateBook = new
        {
            Id = createBookResult.Id,
            Stock = 20
        };
        var content = new StringContent(JsonSerializer.Serialize(updateBook), Encoding.UTF8, "application/json");
        var response = await client.PatchAsync("/api/book", content);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    #endregion
    #region GetBooks
    [Test]
    public async Task GetBooks_Exists()
    {
        // Act        
        var response = await client.GetAsync($"/api/books?pageIndex={1}&pageSize={10}");

        // Assert
        Assert.That(response.StatusCode, Is.Not.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GetBooks_GetOrders()
    {

        // Act        
        var createBookResult = await TestUtils.CreateBook(client);

        var response = await client.GetAsync($"/api/books?pageIndex={1}&pageSize={10}");

        // Assert        
        response.EnsureSuccessStatusCode();
        var contentString = await response.Content.ReadAsStringAsync();
        var books = JsonSerializer.Deserialize<PagingResult<GetBooksQueryResult>>(contentString, TestUtils.JsonOptions);

        Assert.IsNotNull(books);
        Assert.IsNotEmpty(books.Data);
    }

    #endregion
}
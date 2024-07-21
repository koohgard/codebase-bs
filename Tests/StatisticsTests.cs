using System.Net;
using System.Text.Json;
using Abstraction.Command.Order.GetOrders;
using Abstraction.Command.Order.GetStatistics;

namespace Tests;
[TestFixture]
public class StatisticsTests
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

    #region GetStatisticss
    [Test]
    public async Task GetStatistics_Exists()
    {
        // Act        
        var response = await client.GetAsync($"/api/statistics");

        // Assert
        Assert.That(response.StatusCode, Is.Not.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GetStatistics_GetStatistics()
    {
        // Act        
        await TestUtils.CreateOrder(client);
        
        var response = await client.GetAsync($"/api/statistics");

        // Assert        
        response.EnsureSuccessStatusCode();
        var contentString = await response.Content.ReadAsStringAsync();
        var statistics = JsonSerializer.Deserialize<IEnumerable<GetOrderStatisticsQueryResult>>(contentString, TestUtils.JsonOptions);

        Assert.IsNotNull(statistics);
        Assert.IsNotEmpty(statistics);
    }

    #endregion
}
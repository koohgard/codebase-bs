using System.Data.Common;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Tests;
[TestFixture]
public class CustomerTests
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

    #region Register
    [Test]
    public async Task Customer_Register_Exists()
    {
        // Act
        var newCustomer = new { };
        var content = new StringContent(JsonSerializer.Serialize(newCustomer), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/customer/register", content);

        // Assert
        Assert.IsTrue(response.IsSuccessStatusCode, "Register Customer API does not exist or is not accessible.");
    }

    [Test]
    public async Task Customer_Register_ConfirmPassword_ReturnsValidationError()
    {
        // Arrange
        var newStation = new
        {
            Email = "testUser@gmail.com",
            Password = "testPassword",
            ConfirmPassword = "testP"
        };
        var content = new StringContent(JsonSerializer.Serialize(newStation), Encoding.UTF8, "application/json");
        // Act
        var response = await client.PostAsync("/api/customers/register", content);
        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Customer_Register_PasswordLength_ReturnsValidationError()
    {
        // Arrange
        var newStation = new
        {
            Email = "testUser@gmail.com",
            Password = "test",
            ConfirmPassword = "test"
        };
        var content = new StringContent(JsonSerializer.Serialize(newStation), Encoding.UTF8, "application/json");
        // Act
        var response = await client.PostAsync("/api/customers/register", content);
        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Customer_Register()
    {
        // Act
        var newCustomer = new
        {
            Email = "testUser@gmail.com",
            Password = "test",
            ConfirmPassword = "test"
        };
        var content = new StringContent(JsonSerializer.Serialize(newCustomer), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/customer/register", content);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    #endregion
    #region Login
    [Test]
    public async Task Customer_Login_Exists()
    {
        // Act
        var loginData= new { };
        var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/customer/login", content);

        // Assert
        Assert.IsTrue(response.IsSuccessStatusCode, "Login Customer API does not exist or is not accessible.");
    }
    
    [Test]
    public async Task Customer_Login()
    {
        // Act
        var newCustomer = new
        {
            Email = "testUser@gmail.com",
            Password = "testPassword",
        };
        var registerContent = new StringContent(JsonSerializer.Serialize(newCustomer), Encoding.UTF8, "application/json");
        var registerResponse = await client.PostAsync("/api/customer/register", registerContent);


        var loginData = new
        {
            Email = "testUser@gmail.com",
            Password = "testPassword"
        };
        var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/customer/login", content);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task Customer_Login_IncorrectUserNamePassword()
    {
        // Act
        var newCustomer = new
        {
            Email = "testUser@gmail.com",
            Password = "testPassword",
        };
        var registerContent = new StringContent(JsonSerializer.Serialize(newCustomer), Encoding.UTF8, "application/json");
        var registerResponse = await client.PostAsync("/api/customer/register", registerContent);


        var loginData = new
        {
            Email = "testUser@gmail.com",
            Password = "testPass"
        };
        var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/customer/login", content);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }
    #endregion
}
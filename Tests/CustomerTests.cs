﻿using System.Data.Common;
using System.Net;
using System.Text;
using System.Text.Json;
using Abstraction.Command.Customer.CustomerOrders;

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
        Assert.That(response.StatusCode, Is.Not.EqualTo(HttpStatusCode.NotFound));
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
        var response = await client.PostAsync("/api/customer/register", content);
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
        var response = await client.PostAsync("/api/customer/register", content);
        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Customer_Register_DuplicateEmail()
    {
        // Act
        var newCustomer = new
        {
            Email = "testUser@gmail.com",
            Password = "testPassword",
            ConfirmPassword = "testPassword"
        };
        var content = new StringContent(JsonSerializer.Serialize(newCustomer), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/customer/register", content);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var duplicateResponse = await client.PostAsync("/api/customer/register", content);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
    }

    [Test]
    public async Task Customer_Register()
    {
        // Act
        var newCustomer = new
        {
            Email = "testUser@gmail.com",
            Password = "testPassword",
            ConfirmPassword = "testPassword"
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
        var loginData = new { };
        var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/customer/login", content);

        // Assert
        Assert.That(response.StatusCode, Is.Not.EqualTo(HttpStatusCode.NotFound));
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
    #region Orders

    [Test]
    public async Task Customer_Orders_Exists()
    {
        // Act        
        var response = await client.GetAsync($"/api/customer/orders?pageIndex={1}&pageSize={10}");

        // Assert
        Assert.That(response.StatusCode, Is.Not.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task Customer_Orders_Unauthorized()
    {
        // Act        
        var response = await client.GetAsync($"/api/customer/orders?pageIndex={1}&pageSize={10}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }


    [Test]
    public async Task Customer_Orders_GetOrders()
    {

        // Act        

        var loginResult = await TestUtils.RegisterAndLogin(client);

        //todo insert orders

        var response = await client.GetAsync($"/api/customer/orders?pageIndex={1}&pageSize={10}");

        // Assert        
        response.EnsureSuccessStatusCode();
        var contentString = await response.Content.ReadAsStringAsync();
        var orders = JsonSerializer.Deserialize<List<CustomerOrdersQueryResult>>(contentString, TestUtils.JsonOptions);

        Assert.IsNotNull(orders);
        Assert.IsNotEmpty(orders);
    }

    #endregion

}
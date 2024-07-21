using System.Text;
using System.Text.Json;
using Abstraction.Command.Book.CreateBook;
using Abstraction.Command.Customer.Login;
using Abstraction.Command.Order.CreateOrder;

namespace Tests;

public static class TestUtils
{
    public static JsonSerializerOptions JsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    public static async Task<LoginCommandResult> RegisterAndLogin(HttpClient client)
    {
        var newCustomer = new
        {
            Email = "testUser@gmail.com",
            Password = "testPassword",
            ConfirmPassword = "testPassword",
        };
        var registerContent = new StringContent(JsonSerializer.Serialize(newCustomer), Encoding.UTF8, "application/json");
        var registerResponse = await client.PostAsync("/api/customer/register", registerContent);
        registerResponse.EnsureSuccessStatusCode();

        var loginData = new
        {
            Email = "testUser@gmail.com",
            Password = "testPassword"
        };
        var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/customer/login", content);
        response.EnsureSuccessStatusCode();
        var stringContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<LoginCommandResult>(stringContent, JsonOptions);
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.Token);
        return result;
    }

    public static async Task<CreateBookCommandResult> CreateBook(HttpClient client)
    {
        await RegisterAndLogin(client);
        var newBook = new
        {
            Title = "BookTitle",
            Description = "Description",
            Price = 100,
            InitStock = 100,
        };
        var content = new StringContent(JsonSerializer.Serialize(newBook), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/book", content);
        response.EnsureSuccessStatusCode();
        var stringContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<CreateBookCommandResult>(stringContent, JsonOptions);
        return result;
    }

    public static async Task<CreateOrderCommandResult> CreateOrder(HttpClient client)
    {
        var createBookResult = await CreateBook(client);
        var newOrder = new
        {
            BookId = createBookResult.Id,
            Count = 1
        };
        var content = new StringContent(JsonSerializer.Serialize(newOrder), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/order", content);
        response.EnsureSuccessStatusCode();
        var stringContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<CreateOrderCommandResult>(stringContent, JsonOptions);
        return result;

    }
}

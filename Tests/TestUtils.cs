using System.Text;
using System.Text.Json;
using Abstraction.Command.Customer.Login;

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
        return result;
    }
}

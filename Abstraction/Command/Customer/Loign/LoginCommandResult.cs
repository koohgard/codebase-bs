namespace Abstraction.Command.Customer.Login;

public class LoginCommandResult
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }

}

using MediatR;

namespace Abstraction.Command.Customer.Login;

public class LoginCommand : IRequest<LoginCommandResult>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

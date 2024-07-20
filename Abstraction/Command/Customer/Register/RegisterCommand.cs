using MediatR;

namespace Abstraction.Command.Customer.Register;

public class RegisterCommand : IRequest<RegisterCommandResult>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

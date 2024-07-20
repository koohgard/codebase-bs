using Abstraction.Command.Customer.Register;
using MediatR;

namespace Application.Customer;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandResult>
{
    public RegisterCommandHandler()
    {

    }

    public async Task<RegisterCommandResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        //todo implement
        throw new NotImplementedException();
    }
}
using Abstraction.Command.Customer.Register;
using MediatR;

namespace Application.Customer;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandResult>
{
    private readonly IMediator mediator;

    public RegisterCommandHandler(IMediator mediator)
    {

        this.mediator = mediator;
    }

    public async Task<RegisterCommandResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        //todo implement
        throw new NotImplementedException();
    }
}
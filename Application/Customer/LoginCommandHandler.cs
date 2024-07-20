using Abstraction.Command.Customer.Login;
using MediatR;

namespace Application;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResult>
{
    private readonly IMediator mediator;

    public LoginCommandHandler(IMediator mediator)
    {

        this.mediator = mediator;
    }

    public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        //todo implement 
        throw new NotImplementedException();
    }
}
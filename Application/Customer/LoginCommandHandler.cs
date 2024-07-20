﻿using Abstraction.Command.Customer.Login;
using MediatR;

namespace Application.Customer;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResult>
{
    public LoginCommandHandler()
    {
    }

    public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        //TODO implement 
        throw new NotImplementedException();
    }
}
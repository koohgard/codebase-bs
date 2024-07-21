using System.Security.Claims;
using Abstraction.Command.Customer.Login;
using Abstraction.Exception;
using AutoMapper;
using Domain.Entity;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customer;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResult>
{
    private readonly AppDbContext appDbContext;
    public LoginCommandHandler(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;        
    }

    public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await this.appDbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (user != null)
        {
            var hashedPassword = Utils.MD5Hash(request.Password);
            if (user.Password == hashedPassword)
            {
                var token = await GenerateToken(user);
                var result = new LoginCommandResult()
                {
                    Email = request.Email,
                    Token = token
                };
            }
        }
        throw new LoginFaildException();
    }

    private async Task<string> GenerateToken(User user)
    {
        var claims = new List<Claim>
            {
                new(type: "user-id", value: user.Id.ToString()),
                new(type: "type", value: user.UserType.ToString()),
            };
        var jwtToken = "";
        return jwtToken;
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Abstraction.Command.Customer.Login;
using Abstraction.Common;
using Abstraction.Exception;
using Domain.Entity;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;



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
                return result;
            }
        }
        //TODO login faild and ....         
        throw new LoginFaildException();
    }

    private async Task<string> GenerateToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(Utils.JwtSettings.SecretKey);
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
            {
                new(type: "user-id", value: user.Id.ToString()),
                new(type: "type", value: user.UserType.ToString()),
            };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            Audience = Utils.JwtSettings.Audience,
            Issuer = Utils.JwtSettings.Issuer,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }
}
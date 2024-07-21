using Abstraction.Command.Customer.Register;
using Abstraction.Enum;
using Abstraction.Exception;
using AutoMapper;
using Domain.Entity;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customer;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandResult>
{
    private readonly AppDbContext appDbContext;
    private readonly IMapper mapper;

    public RegisterCommandHandler(AppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }
    public async Task<RegisterCommandResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existUser = await this.appDbContext.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (existUser)
        {
            throw new DuplicateEmailException();
        }
        var user = this.mapper.Map<User>(request);
        user.Password = Utils.MD5Hash(request.Password);
        user.UserType = UserType.Customer;
        user.LockoutDateTime = DateTime.Now;
        user.LoginFaildCount = 0;
        await appDbContext.AddAsync(user, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);
        return mapper.Map<RegisterCommandResult>(user);
    }
}
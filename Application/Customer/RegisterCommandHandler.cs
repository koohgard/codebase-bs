using Abstraction.Command.Customer.Register;
using AutoMapper;
using Domain.Entity;
using Infrastructure.Context;
using MediatR;

namespace Application.Customer;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandResult>
{
    private readonly AppDbContext context;
    private readonly IMapper mapper;

    public RegisterCommandHandler(AppDbContext appDbContext, IMapper mapper)
    {
        this.context = appDbContext;
        this.mapper = mapper;
    }
    public async Task<RegisterCommandResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = this.mapper.Map<User>(request);
        //TODO implement
        throw new NotImplementedException();
    }
}
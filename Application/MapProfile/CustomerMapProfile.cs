using Abstraction.Command.Customer.Register;
using AutoMapper;
using Domain.Entity;

namespace Application.MapProfile;

public class CustomerMapProfile : Profile
{
    public CustomerMapProfile()
    {
        CreateMap<RegisterCommand, User>();
        CreateMap<User, RegisterCommandResult>();
    }
}


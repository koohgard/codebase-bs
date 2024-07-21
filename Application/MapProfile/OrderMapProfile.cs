using Abstraction.Command.Order.CreateOrder;
using AutoMapper;
using Domain.Entity;

namespace Application.MapProfile;

public class OrderMapProfile : Profile
{
    public OrderMapProfile()
    {
        CreateMap<CreateOrderCommand, Order>();
        CreateMap<Order, CreateOrderCommandResult>();
    }
}


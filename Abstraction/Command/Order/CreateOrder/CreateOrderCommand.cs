using MediatR;

namespace Abstraction.Command.Order.CreateOrder;

public class CreateOrderCommand : IRequest<CreateOrderCommandResult>
{
}

using MediatR;

namespace Abstraction.Command.Order.CreateOrder;

public class CreateOrderCommand : IRequest<CreateOrderCommandResult>
{
    public int BookId { get; set; }
    public int Count{ get; set; }
}

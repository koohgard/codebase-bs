using MediatR;

namespace Abstraction.Command.Order.GetOrder;

public class GetOrderQuery : IRequest<GetOrderQueryResult>
{
    public int Id { get; set; }
}

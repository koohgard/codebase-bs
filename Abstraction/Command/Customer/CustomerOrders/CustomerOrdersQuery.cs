using MediatR;

namespace Abstraction.Command.Customer.CustomerOrders;

public class CustomerOrdersQuery : IRequest<IEnumerable<CustomerOrdersQueryResult>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}

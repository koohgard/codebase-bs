using MediatR;

namespace Abstraction.Command.Customer.GetCustomerOrders;

public class GetCustomerOrdersQuery : IRequest<PagingResult<GetCustomerOrdersQueryResult>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}

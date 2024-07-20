using MediatR;

namespace Abstraction.Command.Order.GetOrders;

public class GetOrdersQuery : IRequest<PagingResult<GetOrdersQueryResult>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

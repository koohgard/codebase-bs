using MediatR;

namespace Abstraction.Command.Order.GetOrders;

public class GetOrderStatisticsQuery : IRequest<IEnumerable<GetOrderStatisticsQueryResult>>
{
}

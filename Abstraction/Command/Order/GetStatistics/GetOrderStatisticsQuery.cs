using MediatR;

namespace Abstraction.Command.Order.GetStatistics;

public class GetOrderStatisticsQuery : IRequest<IEnumerable<GetOrderStatisticsQueryResult>>
{
}

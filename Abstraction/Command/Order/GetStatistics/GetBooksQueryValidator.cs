using FluentValidation;

namespace Abstraction.Command.Order.GetStatistics;

public class GetOrderStatisticsQueryValidator : AbstractValidator<GetOrderStatisticsQuery>
{
    public GetOrderStatisticsQueryValidator()
    {
    }

}

using FluentValidation;

namespace Abstraction.Command.Order.GetOrders;

public class GetOrderStatisticsQueryValidator : AbstractValidator<GetOrderStatisticsQuery>
{
    public GetOrderStatisticsQueryValidator()
    {
    }

}

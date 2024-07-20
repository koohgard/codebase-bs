using FluentValidation;

namespace Abstraction.Command.Order.GetOrders;

public class GetOrdersQueryValidator : AbstractValidator<GetOrdersQuery>
{
    public GetOrdersQueryValidator()
    {
        RuleFor(x => x.PageIndex).GreaterThan(1);
        RuleFor(x => x.PageSize).GreaterThan(0);
        RuleFor(x => x.StartDate).NotNull();
        RuleFor(x => x.EndDate).NotNull();
        RuleFor(x => x)
           .Must(x => x.StartDate < x.EndDate)
           .WithMessage("EndDate must be greater than StartDate.");
    }

}

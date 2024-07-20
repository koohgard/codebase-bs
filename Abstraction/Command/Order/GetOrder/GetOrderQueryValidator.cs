using FluentValidation;

namespace Abstraction.Command.Order.GetOrder;

public class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>
{
    public GetOrderQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

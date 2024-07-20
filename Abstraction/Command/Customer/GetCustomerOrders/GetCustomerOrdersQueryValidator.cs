using FluentValidation;

namespace Abstraction.Command.Customer.GetCustomerOrders;

public class GetCustomerOrdersQueryValidator : AbstractValidator<GetCustomerOrdersQuery>
{
    public GetCustomerOrdersQueryValidator()
    {
        RuleFor(x => x.PageIndex).GreaterThan(1);
        RuleFor(x => x.PageSize).GreaterThan(0);
    }

}

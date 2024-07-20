using FluentValidation;

namespace Abstraction.Command.Customer.CustomerOrders;

public class CustomerOrdersQueryValidator : AbstractValidator<CustomerOrdersQuery>
{
    public CustomerOrdersQueryValidator()
    {
        RuleFor(x => x.PageIndex).GreaterThan(1);
        RuleFor(x => x.PageSize).GreaterThan(0);
    }

}

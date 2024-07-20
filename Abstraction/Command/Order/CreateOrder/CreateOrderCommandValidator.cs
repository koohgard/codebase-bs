
using FluentValidation;

namespace Abstraction.Command.Order.CreateOrder;
public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
    }
}

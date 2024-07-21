
using FluentValidation;

namespace Abstraction.Command.Book.UpdateStock;
public class UpdateStockCommandValidator : AbstractValidator<UpdateStockCommand>
{
    public UpdateStockCommandValidator()
    {
        RuleFor(x => x.BookId).GreaterThan(0);
        RuleFor(x => x.Count).GreaterThanOrEqualTo(0);
    }
}

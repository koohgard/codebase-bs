
using FluentValidation;

namespace Abstraction.Command.Book.CreateBook;
public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Description).NotEmpty().MinimumLength(8).MaximumLength(10000);
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.InitStock).GreaterThanOrEqualTo(0);
    }
}

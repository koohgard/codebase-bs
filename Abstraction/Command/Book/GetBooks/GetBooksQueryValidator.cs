using FluentValidation;

namespace Abstraction.Command.Book.GetBooks;

public class GetBooksQueryValidator : AbstractValidator<GetBooksQuery>
{
    public GetBooksQueryValidator()
    {
        RuleFor(x => x.PageIndex).GreaterThan(1);
        RuleFor(x => x.PageSize).GreaterThan(0);
    }

}

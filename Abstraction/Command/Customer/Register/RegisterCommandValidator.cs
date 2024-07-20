
using FluentValidation;

namespace Abstraction.Command.Customer.Register;
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(255);
        RuleFor(x => x.ConfirmPassword).NotEmpty().MinimumLength(8).MaximumLength(255);
        RuleFor(x => x)
           .Must(x => x.Password == x.ConfirmPassword)
           .WithMessage("Password and confirmation password do not match.");
    }
}

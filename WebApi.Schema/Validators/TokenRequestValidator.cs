using FluentValidation;

namespace WebApi.Schema;

public class TokenRequestValidator : AbstractValidator<TokenRequest>
{
    public TokenRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("User name must not be empty")
            .NotNull().WithMessage("User name must not be empty")
            .MaximumLength(20).WithMessage("User name must not exceed 20 characters");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password must not be empty")
            .NotNull().WithMessage("Password must not be empty")
            .MinimumLength(8).WithMessage("Password must not be less than 8 characters")
            .MaximumLength(30).WithMessage("Password must not exceed 30 characters");

    }
}

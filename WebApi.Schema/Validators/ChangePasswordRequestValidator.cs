using FluentValidation;

namespace WebApi.Schema;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New password must not be empty")
            .NotNull().WithMessage("New password must not be empty")
            .MinimumLength(8).WithMessage("New password must not be less than 8 characters")
            .MaximumLength(30).WithMessage("New password must not exceed 30 characters");

        RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage("Old password must not be empty")
            .NotNull().WithMessage("Old password must not be empty")
            .MinimumLength(8).WithMessage("Old password must not be less than 8 characters")
            .MaximumLength(30).WithMessage("Old password must not exceed 30 characters");
    }
}

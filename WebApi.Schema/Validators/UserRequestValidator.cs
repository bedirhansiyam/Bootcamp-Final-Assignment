using FluentValidation;

namespace WebApi.Schema;

public class UserRequestValidator : AbstractValidator<UserRequest>
{
    public UserRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("User name must not be empty")
            .NotNull().WithMessage("User name must not be empty")
            .MinimumLength(5).WithMessage("User name must not be less than 5 characters")
            .MaximumLength(20).WithMessage("User name must not exceed 20 characters");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name must not be empty")
            .NotNull().WithMessage("Name must not be empty")
            .MinimumLength(3).WithMessage("Name must not be less than 3 characters")
            .MaximumLength(20).WithMessage("Name must not exceed 20 characters");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Surname must not be empty")
            .NotNull().WithMessage("Surname must not be empty")
            .MinimumLength(3).WithMessage("Surname must not be less than 3 characters")
            .MaximumLength(20).WithMessage("Surname must not exceed 20 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email must not be empty")
            .NotNull().WithMessage("Email must not be empty")
            .MinimumLength(8).WithMessage("Email must not be less than 8 characters")
            .MaximumLength(30).WithMessage("Email must not exceed 30 characters");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password must not be empty")
            .NotNull().WithMessage("Password must not be empty")
            .MinimumLength(8).WithMessage("Password must not be less than 8 characters")
            .MaximumLength(30).WithMessage("Password must not exceed 30 characters");
    }
}

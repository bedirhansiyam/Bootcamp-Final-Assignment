using FluentValidation;

namespace WebApi.Schema;

public class CouponRequestValidator : AbstractValidator<CouponRequest>
{
    public CouponRequestValidator()
    {
        RuleFor(x => x.DueDate)
            .NotEmpty().WithMessage("Due date must not be empty")
            .NotNull().WithMessage("Due date must not be empty")
            .GreaterThan(DateTime.Now).WithMessage("Due date must not be past date");

        RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("Amount must not be empty")
            .NotNull().WithMessage("Amount must not be empty")
            .GreaterThan(0).WithMessage("Amount must be greater than 0");
    }
}

using FluentValidation;

namespace WebApi.Schema;

public class AssignCouponRequestValidator : AbstractValidator<AssignCouponRequest>
{
    public AssignCouponRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("User name must not be empty")
            .NotNull().WithMessage("User name must not be empty")
            .MaximumLength(20).WithMessage("User name must not exceed 20 characters");

        RuleFor(x => x.CouponId)
            .NotEmpty().WithMessage("Coupon id must not be empty")
            .NotNull().WithMessage("Coupon id must not be empty")
            .GreaterThan(0).WithMessage("Coupon id must be greater than 0");
    }
}

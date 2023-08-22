using FluentValidation;

namespace WebApi.Schema;

public class OrderRequestValidator : AbstractValidator<OrderRequest>
{
    public OrderRequestValidator()
    {
        RuleFor(x => x.CouponCode)
            .Length(7).WithMessage("Coupon code must be 7 characters");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address must not be empty")
            .NotNull().WithMessage("Address must not be empty")
            .MinimumLength(7).WithMessage("Address must not be less than 7 characters")
            .MaximumLength(250).WithMessage("Address must not exceed 250 characters");
    }
}

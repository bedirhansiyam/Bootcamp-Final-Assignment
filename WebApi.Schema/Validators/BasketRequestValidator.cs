using FluentValidation;

namespace WebApi.Schema;

public class BasketRequestValidator : AbstractValidator<BasketRequest>
{
    public BasketRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Product id must not be empty")
            .NotNull().WithMessage("Product id must not be empty")
            .GreaterThan(0).WithMessage("Product id must be greater than 0");

        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("Quantity must not be empty")
            .NotNull().WithMessage("Quantity must not be empty")
            .GreaterThan(0).WithMessage("Quantity must be greater than 0");
    }
}

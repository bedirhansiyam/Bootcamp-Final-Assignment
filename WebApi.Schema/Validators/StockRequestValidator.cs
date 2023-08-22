using FluentValidation;

namespace WebApi.Schema;

public class StockRequestValidator : AbstractValidator<StockRequest>
{
    public StockRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Product id must not be empty")
            .NotNull().WithMessage("Product id must not be empty")
            .GreaterThan(0).WithMessage("Product id must be greater than 0");

        RuleFor(x => x.NumberOfStock)
            .NotEmpty().WithMessage("Stock must not be empty")
            .NotNull().WithMessage("Stock must not be empty")
            .GreaterThan(0).WithMessage("Stock must be greater than 0");
    }
}

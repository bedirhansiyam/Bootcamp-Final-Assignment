using FluentValidation;

namespace WebApi.Schema;

public class ProductRequestValidator : AbstractValidator<ProductRequest>
{
    public ProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name must not be empty")
            .NotNull().WithMessage("Product name must not be empty")
            .MinimumLength(4).WithMessage("Product name must not be less than 4 characters")
            .MaximumLength(30).WithMessage("Product name must not exceed 30 characters");

        RuleFor(x => x.Description)
            .MinimumLength(10).WithMessage("Description must not be less than 10 characters")
            .MaximumLength(200).WithMessage("Description must not exceed 200 characters");

        RuleFor(x => x.ProducerCompany)
            .MinimumLength(3).WithMessage("Producer company name must not be less than 3 characters")
            .MaximumLength(40).WithMessage("Producer company name must not exceed 40 characters");

        RuleFor(x => x.ReleaseDate)
            .LessThan(DateTime.Now).WithMessage("Release date must not be future date");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price must not be empty")
            .NotNull().WithMessage("Price must not be empty")
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock must be equal or greater than 0");

        RuleFor(x => x.MaxPointsEarned)
            .NotEmpty().WithMessage("Max points earned field must not be empty")
            .NotNull().WithMessage("Max points earned field must not be empty")
            .GreaterThanOrEqualTo(0).WithMessage("Max points earned field must be equal or greater than 0");

        RuleFor(x => x.PercentageOfPoints)
            .NotEmpty().WithMessage("Percentage of points field must not be empty")
            .NotNull().WithMessage("Percentage of points field must not be empty")
            .GreaterThanOrEqualTo(0).WithMessage("Percentage of points field must be equal or greater than 0")
            .LessThan(100).WithMessage("Percentage of points field must be less than 100");

        RuleFor(x => x.IsActive)
            .NotEmpty().WithMessage("IsActive field must not be empty")
            .NotNull().WithMessage("IsActive field must not be empty");
    }
}

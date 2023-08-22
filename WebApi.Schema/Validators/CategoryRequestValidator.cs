using FluentValidation;

namespace WebApi.Schema;
public class CategoryRequestValidator : AbstractValidator<CategoryRequest>
{
    public CategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name must not be empty")
            .NotNull().WithMessage("Category name must not be empty")
            .MinimumLength(3).WithMessage("Category name must not be less than 3 characters")
            .MaximumLength(25).WithMessage("Category name must not exceed 25 characters");

        RuleFor(x => x.Tag)
            .MinimumLength(3).WithMessage("Tag must not be less than 3 characters")
            .MaximumLength(70).WithMessage("Tag must not exceed 70 characters");

        RuleFor(x => x.Url)
            .MinimumLength(3).WithMessage("Url must not be less than 3 characters")
            .MaximumLength(30).WithMessage("Url must not exceed 30 characters");
    }
}

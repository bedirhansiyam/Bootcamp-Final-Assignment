using FluentValidation;

namespace WebApi.Schema;

public class PaymentRequestValidator : AbstractValidator<PaymentRequest>
{
    public PaymentRequestValidator()
    {
        RuleFor(x => x.CardNumber)
            .NotEmpty().WithMessage("Card number must not be empty")
            .NotNull().WithMessage("Card number must not be empty")
            .MinimumLength(16).WithMessage("Card number must not be less than 16 characters")
            .MaximumLength(19).WithMessage("Card number must not exceed 19 characters");
            
        RuleFor(x => x.CardHolder)
            .NotEmpty().WithMessage("Card holder name must not be empty")
            .NotNull().WithMessage("Card holder name must not be empty")
            .MinimumLength(3).WithMessage("Card holder name must not be less than 3 characters")
            .MaximumLength(25).WithMessage("Card holder name must not exceed 25 characters");

        RuleFor(x => x.Cvv)
            .NotEmpty().WithMessage("Cvv must not be empty")
            .NotNull().WithMessage("Cvv must not be empty")
            .GreaterThan(99).WithMessage("Cvv must be 3 digits")
            .LessThan(1000).WithMessage("Cvv must be 3 digits");

        RuleFor(x => x.ExpireYear)
            .NotEmpty().WithMessage("Expire year must not be empty")
            .NotNull().WithMessage("Expire year must not be empty")
            .GreaterThan(2022).WithMessage("Expire year must be before 2022")
            .LessThan(2045).WithMessage("Expire year must be before 2045");

        RuleFor(x => x.ExpireMonth)
            .NotEmpty().WithMessage("Expire month must not be empty")
            .NotNull().WithMessage("Expire month must not be empty")
            .GreaterThan(0).WithMessage("Expire month must not be less than 0")
            .LessThan(12).WithMessage("Expire month must not be greater than 12");
    }
}

using WebApi.Schema;

namespace WebApi.Test.Validators;

public class CouponRequestValidatorTest
{
    [Fact]
    public void WhenValidInputsAreGiven_CouponRequestValidator_ErrorCountShouldBeZero()
    {
        CouponRequest request = new();
        request.DueDate = DateTime.Now.AddMonths(2);
        request.Amount = 10;

        CouponRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.Equal(0, result.Errors.Count);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(0)]
    [InlineData(-10)]
    public void WhenInvalidInputsAreGiven_CouponRequestValidator_ErrorCountShouldBeMoreThanZero(int amount)
    {
        CouponRequest request = new();
        request.DueDate = DateTime.Now.AddMonths(1);
        request.Amount = amount;

        CouponRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.NotEqual(0, result.Errors.Count);
    }

    [Fact]
    public void WhenInvalidDueDatesAreGiven_CouponRequestValidator_ErrorCountShouldBeMoreThanZero()
    {
        CouponRequest request = new();
        request.DueDate = DateTime.Now.AddDays(-20);
        request.Amount = 20;

        CouponRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.NotEqual(0, result.Errors.Count);
    }
}

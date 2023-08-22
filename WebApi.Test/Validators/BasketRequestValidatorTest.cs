using WebApi.Schema;

namespace WebApi.Test.Validators;

public class BasketRequestValidatorTest
{
    [Fact]
    public void WhenValidInputsAreGiven_BasketRequestValidator_ErrorCountShouldBeZero()
    {
        BasketRequest request = new();
        request.Quantity = 1;
        request.ProductId = 1;

        BasketRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.Equal(0, result.Errors.Count);
    }

    [Theory]
    [InlineData(1,0)]
    [InlineData(0,1)]
    [InlineData(-1,-1)]
    [InlineData(null,1)]
    [InlineData(1,null)]
    public void WhenInvalidInputsAreGiven_BasketRequestValidator_ErrorCountShouldBeMoreThanZero(int quantity, int productId)
    {
        BasketRequest request = new();
        request.Quantity = quantity;
        request.ProductId = productId;

        BasketRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.NotEqual(0, result.Errors.Count);
    }
}

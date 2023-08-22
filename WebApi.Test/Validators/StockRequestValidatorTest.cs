using WebApi.Schema;

namespace WebApi.Test.Validators;

public class StockRequestValidatorTest
{
    [Fact]
    public void WhenValidInputsAreGiven_StockRequestValidator_ErrorCountShouldBeZero()
    {
        StockRequest request = new();
        request.ProductId = 1;
        request.NumberOfStock = 10;

        StockRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.Equal(0, result.Errors.Count);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(0, 1)]
    [InlineData(-1, -1)]
    [InlineData(null, 1)]
    [InlineData(1, null)]
    public void WhenInvalidInputsAreGiven_StockRequestValidator_ErrorCountShouldBeMoreThanZero(int productId, int numberOfStock)
    {
        StockRequest request = new();
        request.ProductId = productId;
        request.NumberOfStock = numberOfStock;

        StockRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.NotEqual(0, result.Errors.Count);
    }
}

using WebApi.Schema;

namespace WebApi.Test.Validators;

public class OrderRequestValidatorTest
{
    [Fact]
    public void WhenValidInputsAreGiven_OrderRequestValidator_ErrorCountShouldBeZero()
    {
        OrderRequest request = new();
        request.CouponCode = "coupons";
        request.Address = "addresstest";

        OrderRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.Equal(0, result.Errors.Count);
    }

    [Theory]
    [InlineData("coupons", "")]
    [InlineData("coupons", null)]
    [InlineData("coupons", "ad")]
    [InlineData("coupons", "addresstestaddresstestaddresstestaddresstestaddresstestaddresstestaddresstestaddresstestaddresstestaddresstestaddresstestaddresstestaddresstestaddresstestaddresstestaddresstestaddresstestaddresstestaddresstestaddresstestaddresstestaddresstestaddresstest")]
    [InlineData("co", "addresstest")]
    [InlineData("couponcode", "addresstest")]
    public void WhenInvalidInputsAreGiven_OrderRequestValidator_ErrorCountShouldBeMoreThanZero(string couponCode, string address)
    {
        OrderRequest request = new();
        request.CouponCode = couponCode;
        request.Address = address;

        OrderRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.NotEqual(0, result.Errors.Count);
    }
}

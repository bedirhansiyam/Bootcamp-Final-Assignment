using WebApi.Schema;

namespace WebApi.Test.Validators;

public class AssignCouponRequestValidatorTest
{
    [Fact]
    public void WhenValidInputsAreGiven_AssignCouponRequestValidator_ErrorCountShouldBeZero()
    {
        AssignCouponRequest request = new();
        request.UserName = "usernametest";
        request.CouponId = 1;

        AssignCouponRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.Equal(0, result.Errors.Count);
    }

    [Theory]
    [InlineData("", 1)]
    [InlineData(null, 1)]
    [InlineData("usernametestusernametest", 1)]
    [InlineData("usernametestusernametest", 0)]
    [InlineData("usernametest", null)]
    [InlineData("usernametest", -1)]
    public void WhenInvalidInputsAreGiven_AssignCouponRequestValidator_ErrorCountShouldBeMoreThanZero(string userName, int couponId)
    {
        AssignCouponRequest request = new();
        request.UserName = userName;
        request.CouponId = couponId;

        AssignCouponRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.NotEqual(0, result.Errors.Count);
    }
}

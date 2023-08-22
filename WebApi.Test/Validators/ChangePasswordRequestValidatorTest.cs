using WebApi.Schema;

namespace WebApi.Test.Validators;
public class ChangePasswordRequestValidatorTest
{
    [Fact]
    public void WhenValidInputsAreGiven_ChangePasswordRequestValidator_ErrorCountShouldBeZero()
    {
        ChangePasswordRequest request = new();
        request.NewPassword = "newpasswordtest";
        request.OldPassword = "oldpasswordtest";

        ChangePasswordRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.Equal(0, result.Errors.Count);
    }

    [Theory]
    [InlineData("", "oldpasswordtest")]
    [InlineData("newpasswordtest", "")]
    [InlineData(null, "oldpasswordtest")]
    [InlineData("newpasswordtest", null)]
    [InlineData("newpasswordtest", "oldpasswordtestoldpasswordtestoldpasswordtest")]
    [InlineData("newpasswordtestnewpasswordtestnewpasswordtest", "oldpasswordtest")]
    [InlineData("new", "oldpasswordtest")]
    [InlineData("newpasswordtest", "old")]
    public void WhenInvalidInputsAreGiven_ChangePasswordRequestValidator_ErrorCountShouldBeMoreThanZero(string newPassword, string oldPassword)
    {
        ChangePasswordRequest request = new();
        request.NewPassword = newPassword;
        request.OldPassword = oldPassword;

        ChangePasswordRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.NotEqual(0, result.Errors.Count);
    }
}


using WebApi.Schema;

namespace WebApi.Test.Validators;

public class TokenRequestValidatorTest
{
    [Fact]
    public void WhenValidInputsAreGiven_TokenRequestValidator_ErrorCountShouldBeZero()
    {
        TokenRequest request = new();
        request.UserName = "usernametest";
        request.Password = "passwordtest";

        TokenRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.Equal(0, result.Errors.Count);
    }

    [Theory]
    [InlineData("", "passwordtest")]
    [InlineData("usernametest", "")]
    [InlineData(null, "passwordtest")]
    [InlineData("usernametest", null)]
    [InlineData("usernametest", "passwordtestpasswordtestpasswordtest")]
    [InlineData("usernametestusernametest", "passwordtest")]
    [InlineData("usernametest", "pass")]
    public void WhenInvalidInputsAreGiven_TokenRequestValidator_ErrorCountShouldBeMoreThanZero(string userName, string password)
    {
        TokenRequest request = new();
        request.UserName = userName;
        request.Password = password;

        TokenRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.NotEqual(0, result.Errors.Count);
    }
}

using WebApi.Schema;

namespace WebApi.Test.Validators;

public class UserRequestValidatorTest
{
    [Fact]
    public void WhenValidInputsAreGiven_UserRequestValidator_ErrorCountShouldBeZero()
    {
        UserRequest request = new();
        request.UserName = "usernametest";
        request.Name = "nametest";
        request.Surname = "surnametest";
        request.Email = "emailtest";
        request.Password = "passwordtest";


        UserRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.Equal(0, result.Errors.Count);
    }

    [Theory]
    [InlineData("", "nametest", "surnametest", "emailtest", "passwordtest")]
    [InlineData("usernametest", "", "surnametest", "emailtest", "passwordtest")]
    [InlineData("usernametest", "nametest", "", "emailtest", "passwordtest")]
    [InlineData("usernametest", "nametest", "surnametest", "", "passwordtest")]
    [InlineData("usernametest", "nametest", "surnametest", "emailtest", "")]
    [InlineData(null, "nametest", "surnametest", "emailtest", "passwordtest")]
    [InlineData("usernametest", null, "surnametest", "emailtest", "passwordtest")]
    [InlineData("usernametest", "nametest", null, "emailtest", "passwordtest")]
    [InlineData("usernametest", "nametest", "surnametest", null, "passwordtest")]
    [InlineData("usernametest", "nametest", "surnametest", "emailtest", null)]
    [InlineData("user", "nametest", "surnametest", "emailtest", "passwordtest")]
    [InlineData("usernametestusernametest", "nametest", "surnametest", "emailtest", "passwordtest")]
    [InlineData("usernametest", "na", "surnametest", "emailtest", "passwordtest")]
    [InlineData("usernametest", "nametestnametestnametest", "surnametest", "emailtest", "passwordtest")]
    [InlineData("usernametest", "nametest", "su", "emailtest", "passwordtest")]
    [InlineData("usernametest", "nametest", "surnametestsurnametest", "emailtest", "passwordtest")]
    [InlineData("usernametest", "nametest", "surnametest", "email", "passwordtest")]
    [InlineData("usernametest", "nametest", "surnametest", "emailtestemailtestemailtestemailtest", "passwordtest")]
    [InlineData("usernametest", "nametest", "surnametest", "emailtest", "pass")]
    [InlineData("usernametest", "nametest", "surnametest", "emailtest", "passwordtestpasswordtestpasswordtest")]
    public void WhenInvalidInputsAreGiven_UserRequestValidator_ErrorCountShouldBeMoreThanZero(string userName, string name, string surName, string email, string password)
    {
        UserRequest request = new();
        request.UserName = userName;
        request.Name = name;
        request.Surname = surName;
        request.Email = email;
        request.Password = password;

        UserRequestValidator validator = new();
        var result = validator.Validate(request);

        Assert.NotEqual(0, result.Errors.Count);
    }
}

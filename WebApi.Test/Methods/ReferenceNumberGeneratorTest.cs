namespace WebApi.Test.Methods;

public class ReferenceNumberGeneratorTest
{
    [Fact]
    public void WhenMethodIsCalled_ReferenceNumberGenerator_ShouldBeReturnTwelveCharacterString()
    {
        var result = Guid.NewGuid().ToString().ToUpper().Replace("-", "").Substring(0, 12);
        string expectedPattern = "^[A-Z0-9]{12}$";

        Assert.Matches(expectedPattern, result);

    }
}

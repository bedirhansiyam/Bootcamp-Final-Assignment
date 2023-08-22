namespace WebApi.Test.Methods;

public class CouponCodeGeneratorTest
{
    [Fact]
    public void WhenMethodIsCalled_CouponCodeGenerator_ShouldBeReturnSevenCharacterString()
    {
        var result = Guid.NewGuid().ToString().ToUpper().Replace("-", "").Substring(0, 7);
        string expectedPattern = "^[A-Z0-9]{7}$";

        Assert.Matches(expectedPattern, result);

    }
}

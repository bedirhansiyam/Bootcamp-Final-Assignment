using System.Text;

namespace WebApi.Test.Methods;

public class OrderNumberGeneratorTest
{
    [Fact]
    public void WhenMethodIsCalled_OrderNumberGenerator_ShouldBeReturnOnlyDigitEightCharacterString()
    {
        var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5);

        byte[] bytes = Encoding.ASCII.GetBytes(guid);

        StringBuilder orderNumber = new StringBuilder();
        foreach (var item in bytes)
        {
            orderNumber.Append(item.ToString());
        }

        var result = orderNumber.ToString().Substring(0, 8);
        string expectedPattern = "^[0-9]{8}$";

        Assert.Matches(expectedPattern, result);

    }
}

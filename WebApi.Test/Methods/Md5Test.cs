using Microsoft.AspNetCore.Routing;

namespace WebApi.Test.Methods;

public class Md5Test
{
    [Fact]
    public void WhenStringTextIsGiven_Md5Converter_ShouldBeReturnStringHash()
    {
        string text = "md5test";
        string hash = "82da61aa724b5d149a9c5dc8682c2a45";

        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(text);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            var result = Convert.ToHexString(hashBytes).ToLower();

            Assert.Equal(hash, result);
        }

    }
}

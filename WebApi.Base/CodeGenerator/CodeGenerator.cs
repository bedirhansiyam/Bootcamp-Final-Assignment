using System.Text;

namespace WebApi.Base;

public class CodeGenerator
{
    public static string CouponCode()
    {
        return Guid.NewGuid().ToString().ToUpper().Replace("-", "").Substring(0, 7);
    }

    public static string OrderNumber()
    {
        var guid = Guid.NewGuid().ToString().Replace("-","").Substring(0,5);

        byte[] bytes = Encoding.ASCII.GetBytes(guid);
        
        StringBuilder orderNumber = new StringBuilder();
        foreach (var item in bytes)
        {
            orderNumber.Append(item.ToString());
        }

        return orderNumber.ToString().Substring(0, 8);
    }

    public static string ReferenceNumber()
    {
        return Guid.NewGuid().ToString().ToUpper().Replace("-", "").Substring(0, 12);
    }
}

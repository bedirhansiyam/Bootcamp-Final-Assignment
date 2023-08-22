using WebApi.Base;

namespace WebApi.Schema;

public class OrderRequest : BaseRequest
{
    public string CouponCode { get; set; }
    public string Address { get; set; }
}

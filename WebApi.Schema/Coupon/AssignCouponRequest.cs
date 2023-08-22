using WebApi.Base;

namespace WebApi.Schema;

public class AssignCouponRequest : BaseRequest
{
    public string UserName { get; set; }
    public int CouponId { get; set; }
}

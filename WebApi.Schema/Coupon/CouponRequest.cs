using WebApi.Base;

namespace WebApi.Schema;

public class CouponRequest : BaseRequest
{
    public DateTime DueDate { get; set; }
    public decimal Amount { get; set; }
}

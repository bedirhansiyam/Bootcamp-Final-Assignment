using WebApi.Base;
using WebApi.Data;

namespace WebApi.Schema;

public class OrderResponse : BaseResponse
{
    public string OrderNumber { get; set; }
    public DateTime OrderedDate { get; set; }
    public string Address { get; set; }
    public decimal TotalAmount { get; set; }
    public string CouponCode { get; set; }
    public decimal? CouponAmount { get; set; }
    public decimal UserLoyaltyPoints { get; set; }
    public decimal Payment { get; set; }
    public decimal LoyaltyPointsToBeEarned { get; set; }
    public string UserName { get; set; }
}

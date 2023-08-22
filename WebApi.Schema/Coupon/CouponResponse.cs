using WebApi.Base;

namespace WebApi.Schema;

public class CouponResponse : BaseResponse
{
    public string Code { get; set; }
    public DateTime DueDate { get; set; }
    public decimal Amount { get; set; }
    public bool IsUsed { get; set; }
    public string UserName { get; set; }
}

using WebApi.Base;
using WebApi.Data;

namespace WebApi.Schema;

public class UserResponse
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public decimal LoyaltyPoints { get; set; }
    public string Role { get; set; }
    public List<CouponResponse> Coupons { get; set; }
}

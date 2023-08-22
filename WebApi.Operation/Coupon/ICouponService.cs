using System.Security.Claims;
using WebApi.Base;
using WebApi.Data;
using WebApi.Schema;

namespace WebApi.Operation;

public interface ICouponService : IBaseService<Coupon, CouponRequest, CouponResponse>
{
    ApiResponse InsertRange(int numberOfCoupon, CouponRequest request);
    ApiResponse AssignToUser(AssignCouponRequest request);
    ApiResponse<List<CouponResponse>> GetByUserId(string userName);
}

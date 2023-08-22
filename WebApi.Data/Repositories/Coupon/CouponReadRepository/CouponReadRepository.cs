using WebApi.Data.Context;

namespace WebApi.Data.Repositories;

public class CouponReadRepository : ReadRepository<Coupon>, ICouponReadRepository
{
    public CouponReadRepository(WebEfDbContext dbContext) : base(dbContext)
    {
    }
}

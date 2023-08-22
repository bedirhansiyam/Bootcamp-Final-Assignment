using WebApi.Data.Context;

namespace WebApi.Data.Repositories;

public class CouponWriteRepository : WriteRepository<Coupon>, ICouponWriteRepository
{
    public CouponWriteRepository(WebEfDbContext dbContext) : base(dbContext)
    {
    }
}

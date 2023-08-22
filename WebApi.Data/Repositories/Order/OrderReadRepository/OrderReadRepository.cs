using WebApi.Data.Context;

namespace WebApi.Data.Repositories;

public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository
{
    public OrderReadRepository(WebEfDbContext dbContext) : base(dbContext)
    {
    }
}

using System.Net.Http;
using WebApi.Data.Context;

namespace WebApi.Data.Repositories;

public class BasketReadRepository : ReadRepository<Basket>, IBasketReadRepository
{
    public BasketReadRepository(WebEfDbContext dbContext) : base(dbContext)
    {
    }

    public List<Basket> GetByUserId(string userId)
    {
        return Table.Where(x => x.UserId == userId).ToList();
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApi.Data.Context;

namespace WebApi.Data.Repositories;

public class BasketWriteRepository : WriteRepository<Basket>, IBasketWriteRepository
{
    public BasketWriteRepository(WebEfDbContext dbContext) : base(dbContext)
    {
    }
}

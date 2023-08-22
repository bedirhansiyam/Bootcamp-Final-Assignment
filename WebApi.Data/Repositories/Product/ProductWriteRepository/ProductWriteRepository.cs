using WebApi.Data.Context;

namespace WebApi.Data.Repositories;
public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
{
    public ProductWriteRepository(WebEfDbContext dbContext) : base(dbContext)
    {
    }
}

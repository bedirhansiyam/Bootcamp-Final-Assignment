using WebApi.Data.Context;

namespace WebApi.Data.Repositories;

public class CategoryWriteRepository : WriteRepository<Category>, ICategoryWriteRepository
{
    public CategoryWriteRepository(WebEfDbContext dbContext) : base(dbContext)
    {
    }
}

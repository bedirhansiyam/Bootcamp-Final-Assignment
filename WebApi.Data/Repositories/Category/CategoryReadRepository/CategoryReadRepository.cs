using WebApi.Data.Context;

namespace WebApi.Data.Repositories;

public class CategoryReadRepository : ReadRepository<Category>, ICategoryReadRepository
{
    public CategoryReadRepository(WebEfDbContext dbContext) : base(dbContext)
    {
    }
}

using Dapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApi.Base;
using WebApi.Data.Context;

namespace WebApi.Data.Repositories;

public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
{
    public ProductReadRepository(WebEfDbContext dbContext) : base(dbContext)
    {
    }
}

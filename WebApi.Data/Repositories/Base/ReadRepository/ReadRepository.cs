using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Base;
using WebApi.Data.Context;

namespace WebApi.Data.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    protected readonly WebEfDbContext dbContext;
    public ReadRepository(WebEfDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public DbSet<T> Table => dbContext.Set<T>();

    public List<T> GetAll()
    {
        return Table.ToList();
    }

    public List<T> GetAllWithIncludes(params string[] includes)
    {
        var query = Table.AsQueryable();
        if(includes is not null)
            query = includes.Aggregate(query, (current, inc) => current.Include(inc));

        return query.ToList();
    }

    public T GetById(int id)
    {
        return Table.AsNoTracking().FirstOrDefault(x => x.Id == id);
    }

    public T GetByIdWithIncludes(int id, params string[] includes)
    {
        var query = Table.AsQueryable();
        query = includes.Aggregate(query, (current, inc) => current.Include(inc));
        return query.FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<T> Where(Expression<Func<T, bool>> predicate)
    {
        return Table.Where(predicate).AsQueryable();
    }

    public IEnumerable<T> WhereWithInclude(Expression<Func<T, bool>> predicate, params string[] includes)
    {
        var query = Table.Where(predicate).AsQueryable();
        query = includes.Aggregate(query, (current, inc) => current.Include(inc));
        return query.ToList();
    }
}

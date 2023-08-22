using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApi.Base;
using WebApi.Data.Context;

namespace WebApi.Data.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
{
    protected readonly WebEfDbContext dbContext;
    public WriteRepository(WebEfDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public DbSet<T> Table => dbContext.Set<T>();

    public bool Delete(T entity)
    {
        EntityEntry<T> entityEntry = Table.Remove(entity);
        return entityEntry.State == EntityState.Deleted;
    }

    public bool DeleteById(int id)
    {
        var entity = Table.Find(id);
        EntityEntry<T> entityEntry = Table.Remove(entity);
        return entityEntry.State == EntityState.Deleted;
    }

    public bool Insert(T entity)
    {
        EntityEntry<T> entityEntry = Table.Add(entity);
        return entityEntry.State == EntityState.Added;
    }

    public bool Update(T entity)
    {
        EntityEntry entityEntry = Table.Update(entity);
        return entityEntry.State == EntityState.Modified;
    }

    public void InsertRange(List<T> entityList)
    {
        Table.AddRange(entityList);
    }
    public void DeleteRange(List<T> entityList)
    {
        Table.RemoveRange(entityList);
    }
    public void UpdateRange(List<T> entityList)
    {
        Table.UpdateRange(entityList);
    }    
}

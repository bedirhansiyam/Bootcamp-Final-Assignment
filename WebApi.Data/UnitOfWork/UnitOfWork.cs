using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Data.Entity;
using WebApi.Base;
using WebApi.Data.Context;
using WebApi.Data.Repositories;

namespace WebApi.Data.Uow;

public class UnitOfWork : IUnitOfWork
{
    public IProductReadRepository ProductReadRepository { get; private set; }

    protected readonly WebEfDbContext efDbContext;
    private bool disposed;

    public UnitOfWork(WebEfDbContext efDbContext)
    {
        this.efDbContext = efDbContext;
    }

    public IReadRepository<T> ReadRepository<T>() where T : BaseEntity
    {
        return new ReadRepository<T>(efDbContext);
    }

    public IWriteRepository<T> WriteRepository<T>() where T : BaseEntity
    {
        return new WriteRepository<T>(efDbContext);
    }


    public void Complete()
    {
        efDbContext.SaveChanges();
    }
    public bool CompleteWithTransaction()
    {
        using (var dbDcontextTransaction = efDbContext.Database.BeginTransaction())
        {
            try
            {
                efDbContext.SaveChanges();
                dbDcontextTransaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                dbDcontextTransaction.Rollback();
                return false;
            }
        }
    }

    public void Dispose()
    {
        Clean(true);
    }

    private void Clean(bool disposing)
    {
        if (!disposed)
        {
            if (disposing && efDbContext is not null)
            {
                efDbContext.Dispose();
            }
        }

        disposed = true;
        GC.SuppressFinalize(this);
    }
}

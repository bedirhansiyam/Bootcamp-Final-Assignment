using WebApi.Base;
using WebApi.Data.Repositories;

namespace WebApi.Data.Uow;

public interface IUnitOfWork : IDisposable
{
    IWriteRepository<T> WriteRepository<T>() where T : BaseEntity;
    IReadRepository<T> ReadRepository<T>() where T : BaseEntity;

    void Complete();
    bool CompleteWithTransaction();
}

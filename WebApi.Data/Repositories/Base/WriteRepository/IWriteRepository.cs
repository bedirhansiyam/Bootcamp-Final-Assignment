using WebApi.Base;

namespace WebApi.Data.Repositories;

public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
{
    bool Insert(T entity);
    bool Update(T entity);
    bool Delete(T entity);
    bool DeleteById(int id);
    void InsertRange(List<T> entityList);
    void UpdateRange(List<T> entityList);
    void DeleteRange(List<T> entityList);
}

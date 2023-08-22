using System.Linq.Expressions;
using WebApi.Base;

namespace WebApi.Data.Repositories;

public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
{
    List<T> GetAll();
    List<T> GetAllWithIncludes(params string[] includes);
    T GetByIdWithIncludes(int id, params string[] includes);
    T GetById(int id);
    IEnumerable<T> WhereWithInclude(Expression<Func<T, bool>> predicate, params string[] includes);
    IEnumerable<T> Where(Expression<Func<T, bool>> predicate);

}

using Microsoft.EntityFrameworkCore;
using WebApi.Base;

namespace WebApi.Data.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    DbSet<T> Table { get; }
}

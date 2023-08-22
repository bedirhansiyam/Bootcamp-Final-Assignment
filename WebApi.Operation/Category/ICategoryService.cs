using WebApi.Data;
using WebApi.Schema;

namespace WebApi.Operation;

public interface ICategoryService : IBaseService<Category, CategoryRequest, CategoryResponse>
{
}

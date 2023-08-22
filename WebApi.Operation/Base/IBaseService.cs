using WebApi.Base;

namespace WebApi.Operation;

public interface IBaseService<TEntity, TRequest, TResponse>
{
    ApiResponse<List<TResponse>> GetAll();
    ApiResponse<TResponse> GetById(int id);
    ApiResponse Insert(TRequest request);
    ApiResponse Update(int id, TRequest request);
    ApiResponse Delete(int id);
}

using WebApi.Base;
using WebApi.Data;
using WebApi.Schema;

namespace WebApi.Operation;

public interface IProductService : IBaseService<Product, ProductRequest, ProductResponse>
{
    ApiResponse AddStock(StockRequest request);
    ApiResponse UpdateStock(StockRequest request);
    ApiResponse<List<ProductResponse>> GetByCategory(int categoryId);
    ApiResponse ActivateProduct(int id);
    ApiResponse InactivateProduct(int id);
}

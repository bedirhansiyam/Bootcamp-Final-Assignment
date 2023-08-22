using System.Security.Claims;
using WebApi.Base;
using WebApi.Data;
using WebApi.Schema;

namespace WebApi.Operation;

public interface IOrderService : IBaseService<Order, OrderRequest, OrderResponse>
{
    Task<ApiResponse> Create(ClaimsPrincipal user, OrderRequest orderRequest);
    Task<ApiResponse<List<OrderResponse>>> MyOrders(ClaimsPrincipal user);
    ApiResponse<List<OrderResponse>> GetByUserName(string userName);
}

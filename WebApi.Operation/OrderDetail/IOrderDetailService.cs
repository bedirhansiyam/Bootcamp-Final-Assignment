using System.Security.Claims;
using WebApi.Base;
using WebApi.Schema;

namespace WebApi.Operation;

public interface IOrderDetailService
{
    ApiResponse<List<OrderDetailResponse>> GetAll();
    Task<ApiResponse<List<OrderDetailResponse>>> GetByOrderNumber(ClaimsPrincipal user, string orderNumber);
}

using System.Security.Claims;
using WebApi.Base;
using WebApi.Data;
using WebApi.Schema;

namespace WebApi.Operation;

public interface IBasketService : IBaseService<Basket, BasketRequest, BasketResponse>
{
    ApiResponse<List<BasketResponse>> GetByUserId(string userId);
    ApiResponse<List<BasketResponse>> GetMyBasket(ClaimsPrincipal user);
    ApiResponse ClearBasket(ClaimsPrincipal user);
    ApiResponse RemoveItem(ClaimsPrincipal user, int productId);
    ApiResponse Insert(BasketRequest request, ClaimsPrincipal user);
    ApiResponse Update(ClaimsPrincipal user, BasketRequest request);
}

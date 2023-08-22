using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Base;
using WebApi.Data;
using WebApi.Operation;
using WebApi.Schema;

namespace WebApi.Service;

[Route("webapi/v1/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly IBasketService service;

    public BasketController(IBasketService service)
    {
        this.service = service;
    }

    [Authorize(Policy = Policy.Admin)]
    [HttpGet]
    public ApiResponse<List<BasketResponse>> GetAll()
    {
        return service.GetAll();
    }

    [Authorize(Policy = Policy.Admin)]
    [HttpGet("userId")]
    public ApiResponse<List<BasketResponse>> GetByUserId([FromQuery] string userId)
    {
        return service.GetByUserId(userId);
    }

    [Authorize(Policy = Policy.User)]
    [HttpGet("MyBasket")]
    public ApiResponse<List<BasketResponse>> GetMyBasket()
    {
        return service.GetMyBasket(HttpContext.User);
    }

    [Authorize(Policy = Policy.User)]
    [HttpPost("AddProduct")]
    public ApiResponse Insert([FromBody] BasketRequest request)
    {
        return service.Insert(request, HttpContext.User);
    }

    [Authorize(Policy = Policy.User)]
    [HttpPut("UpdateProduct")]
    public ApiResponse Update([FromBody] BasketRequest request)
    {
        return service.Update(HttpContext.User, request);
    }

    [Authorize(Policy = Policy.User)]
    [HttpDelete("ClearMyBasket")]
    public ApiResponse ClearBasket()
    {
        return service.ClearBasket(HttpContext.User);
    }

    [Authorize(Policy = Policy.User)]
    [HttpDelete("RemoveProduct/{productId}")]
    public ApiResponse RemoveItem(int productId)
    {
        return service.RemoveItem(HttpContext.User, productId);
    }
}

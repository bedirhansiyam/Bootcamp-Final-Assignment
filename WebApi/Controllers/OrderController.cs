using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Base;
using WebApi.Operation;
using WebApi.Schema;

namespace WebApi.Service;

[Route("webapi/v1/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService service;

    public OrderController(IOrderService service)
    {
        this.service = service;
    }

    [Authorize(Policy = Policy.Admin)]
    [HttpGet]
    public ApiResponse<List<OrderResponse>> GetAll()
    {
        return service.GetAll();
    }

    [Authorize(Policy = Policy.Admin)]
    [HttpGet("{id}")]
    public ApiResponse<OrderResponse> GetById(int id)
    {
        return service.GetById(id);
    }

    [Authorize(Policy = Policy.Admin)]
    [HttpGet("ByUserName/{userName}")]
    public ApiResponse<List<OrderResponse>> GetById(string userName)
    {
        return service.GetByUserName(userName);
    }

    [Authorize(Policy = Policy.User)]
    [HttpGet("MyOrders")]
    public async Task<ApiResponse<List<OrderResponse>>> GetMyOrders()
    {
        return await service.MyOrders(HttpContext.User);
    }
    [Authorize(Policy = Policy.User)]
    [HttpPost]
    public async Task<ApiResponse> Insert([FromBody] OrderRequest request)
    {
        return await service.Create(HttpContext.User, request);
    }
}

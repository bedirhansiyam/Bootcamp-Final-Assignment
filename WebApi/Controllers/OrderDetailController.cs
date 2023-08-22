using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Base;
using WebApi.Operation;
using WebApi.Schema;

namespace WebApi.Service;

[Route("webapi/v1/[controller]")]
[ApiController]
public class OrderDetailController : ControllerBase
{
    private readonly IOrderDetailService service;

    public OrderDetailController(IOrderDetailService service)
    {
        this.service = service;
    }
    [Authorize(Policy = Policy.Admin)]
    [HttpGet]
    public ApiResponse<List<OrderDetailResponse>> GetAll()
    {
        return service.GetAll();
    }

    [Authorize(Policy = Policy.AdminAndUser)]
    [HttpGet("{orderNumber}")]
    public async Task<ApiResponse<List<OrderDetailResponse>>> GetByOrderNumber(string orderNumber)
    {
        return await service.GetByOrderNumber(HttpContext.User, orderNumber);
    }
}

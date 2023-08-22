using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Base;
using WebApi.Operation;
using WebApi.Schema;

namespace WebApi.Service;

[Route("webapi/v1/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService service;

    public ProductController(IProductService service)
    {
        this.service = service;
    }

    [HttpGet]
    public ApiResponse<List<ProductResponse>> GetAll()
    {
        return service.GetAll();
    }

    [HttpGet("{id}")]
    public ApiResponse<ProductResponse> GetById(int id) 
    {
        return service.GetById(id);
    }

    [HttpGet("GetByCategory/{categoryId}")]
    public ApiResponse<List<ProductResponse>> GetByCategory(int categoryId)
    {
        return service.GetByCategory(categoryId);
    }

    [Authorize(Policy = Policy.Admin)]
    [HttpPost]
    public ApiResponse Insert([FromBody] ProductRequest request)
    {
        return service.Insert(request);
    }

    [Authorize(Policy = Policy.Admin)]
    [HttpPut("{id}")]
    public ApiResponse Update(int id, [FromBody] ProductRequest request)
    {
        return service.Update(id, request);
    }

    [Authorize(Policy = Policy.Admin)]
    [HttpDelete("{id}")]
    public ApiResponse Delete(int id) 
    {
        return service.Delete(id);
    }

    [Authorize(Policy = Policy.Admin)]
    [HttpPost("AddStock")]
    public ApiResponse AddStock([FromBody] StockRequest request)
    {
        return service.AddStock(request);
    }

    [Authorize(Policy = Policy.Admin)]
    [HttpPut("UpdateStock")]
    public ApiResponse UpdateStock([FromBody] StockRequest request)
    {
        return service.UpdateStock(request);
    }

    [Authorize(Policy = Policy.Admin)]
    [HttpPut("ActivateProduct/{id}")]
    public ApiResponse Activate(int id)
    {
        return service.ActivateProduct(id);
    }

    [Authorize(Policy = Policy.Admin)]
    [HttpPut("InactivateProduct/{id}")]
    public ApiResponse Inactivate(int id)
    {
        return service.InactivateProduct(id);
    }

}

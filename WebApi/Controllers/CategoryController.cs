using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Base;
using WebApi.Operation;
using WebApi.Schema;

namespace WebApi.Service;

[Route("webapi/v1/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService service;

    public CategoryController(ICategoryService service)
    {
        this.service = service;
    }

    [HttpGet]
    public ApiResponse<List<CategoryResponse>> GetAll()
    {
        return service.GetAll();
    }

    [HttpGet("{id}")]
    public ApiResponse<CategoryResponse> GetById(int id)
    {
        return service.GetById(id);
    }

    [Authorize(Policy = Policy.Admin)]
    [HttpPost]
    public ApiResponse Insert([FromBody] CategoryRequest request)
    {
        return service.Insert(request);
    }

    [Authorize(Policy = Policy.Admin)]
    [HttpPut("{id}")]
    public ApiResponse Update(int id, [FromBody] CategoryRequest request)
    {
        return service.Update(id, request);
    }

    [Authorize(Policy = Policy.Admin)]
    [HttpDelete("{id}")]
    public ApiResponse Delete(int id)
    {
        return service.Delete(id);
    }
}

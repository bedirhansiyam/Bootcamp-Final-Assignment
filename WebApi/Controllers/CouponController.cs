using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Base;
using WebApi.Operation;
using WebApi.Schema;

namespace WebApi.Service;

[Route("webapi/v1/[controller]")]
[Authorize(Policy = Policy.Admin)]
[ApiController]
public class CouponController : ControllerBase
{
    private readonly ICouponService service;

    public CouponController(ICouponService service)
    {
        this.service = service;
    }

    [HttpGet]
    public ApiResponse<List<CouponResponse>> GetAll()
    {
        return service.GetAll();
    }

    [HttpGet("{id}")]
    public ApiResponse<CouponResponse> GetById(int id)
    {
        return service.GetById(id);
    }

    [HttpGet("ByUserName/{userName}")]
    public ApiResponse<List<CouponResponse>> GetByUserId(string userName)
    {
        return service.GetByUserId(userName);
    }

    [HttpPost]
    public ApiResponse Insert([FromBody] CouponRequest request)
    {
        return service.Insert(request);
    }

    [HttpPost("AddCoupons/{numberOfCoupon}")]
    public ApiResponse InsertRange(int numberOfCoupon, [FromBody] CouponRequest request)
    {
        return service.InsertRange(numberOfCoupon, request);
    }

    [HttpPut("{id}")]
    public ApiResponse Update(int id, [FromBody] CouponRequest request)
    {
        return service.Update(id, request);
    }

    [HttpPut("AssignToUser")]
    public ApiResponse AssignToUser([FromBody] AssignCouponRequest request)
    {
        return service.AssignToUser(request);
    }

    [HttpDelete("{id}")]
    public ApiResponse Delete(int id)
    {
        return service.Delete(id);
    }
}

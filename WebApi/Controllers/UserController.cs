using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Base;
using WebApi.Operation;
using WebApi.Schema;

namespace WebApi.Service;

[Route("webapi/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService service;

    public UserController(IUserService service)
    {
        this.service = service;
    }

    [Authorize(Policy = Policy.Admin)]
    [HttpGet]
    public async Task<ApiResponse<List<UserResponse>>> GetAll()
    {
        return await service.GetAll();
    }

    [Authorize(Policy = Policy.AdminAndUser)]
    [HttpGet("MyAccount")]
    public async Task<ApiResponse<UserResponse>> GetUser()
    {
        return await service.GetUser(HttpContext.User);
    }

    [Authorize(Policy = Policy.User)]
    [HttpGet("MyLoyaltyPoints")]
    public async Task<ApiResponse<LoyaltyPointsResponse>> GetMyLoyaltyPoints()
    {
        return await service.GetLoyaltyPoints(HttpContext.User);
    }

    [Authorize(Policy = Policy.Admin)]
    [HttpGet("{userName}")]
    public async Task<ApiResponse<UserResponse>> GetByUserName(string userName)
    {
        return await service.GetByUserName(userName);
    }

    [Authorize(Policy = Policy.Admin)]
    [HttpPost("AddUser")]
    public async Task<ApiResponse> PostUser([FromBody] UserRequest request)
    {
        return await service.InsertUser(request);
    }
    [Authorize(Policy = Policy.Admin)]
    [HttpPost("AddAdmin")]
    public async Task<ApiResponse> PostAdmin([FromBody] UserRequest request)
    {
        return await service.InsertAdmin(request);
    }

    [Authorize(Policy = Policy.Admin)]
    [HttpPut("{userId}")]
    public async Task<ApiResponse> Put(string userId, [FromBody] UserRequest request)
    {
        return await service.Update(userId, request);
    }

    [Authorize(Policy = Policy.Admin)]
    [HttpDelete("{userName}")]
    public async Task<ApiResponse> Delete(string userName)
    {
        return await service.Delete(userName);
    }

    [Authorize(Policy = Policy.Admin)]
    [HttpPost("UnlockUser/{userName}")]
    public async Task<ApiResponse> UnlockUser(string userName)
    {
        return await service.UnlockUser(userName);
    }
}

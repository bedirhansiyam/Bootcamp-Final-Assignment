using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Base;
using WebApi.Operation;
using WebApi.Schema;

namespace WebApi.Service;

[Route("webapi/v1/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService service;

    public AuthenticationController(IAuthenticationService service)
    {
        this.service = service;
    }

    [HttpPost("SignIn")]
    public async Task<ApiResponse<TokenResponse>> SignIn([FromBody] TokenRequest request)
    {
        return await service.SignIn(request);
    }

    [Authorize(Policy = Policy.AdminAndUser)]
    [HttpPost("SignOut")]
    public async Task<ApiResponse> SignOut()
    {
        return await service.SignOut();
    }

    [Authorize(Policy = Policy.AdminAndUser)]
    [HttpPost("ChangePassword")]
    public async Task<ApiResponse> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        return await service.ChangePassword(HttpContext.User, request);
    }

    [HttpPost("SignUp")]
    public async Task<ApiResponse> SignUp([FromBody] UserRequest request)
    {
        return await service.Insert(request);
    }
}

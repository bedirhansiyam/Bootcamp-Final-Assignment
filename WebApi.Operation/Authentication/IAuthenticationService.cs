using System.Security.Claims;
using WebApi.Base;
using WebApi.Schema;

namespace WebApi.Operation;

public interface IAuthenticationService
{
    public Task<ApiResponse> Insert(UserRequest request);
    public Task<ApiResponse<TokenResponse>> SignIn(TokenRequest request);
    public Task<ApiResponse> SignOut();
    public Task<ApiResponse> ChangePassword(ClaimsPrincipal user, ChangePasswordRequest request);
}

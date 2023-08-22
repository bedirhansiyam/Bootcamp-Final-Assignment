using System.Security.Claims;
using WebApi.Base;
using WebApi.Schema;

namespace WebApi.Operation;

public interface IUserService
{
    public Task<ApiResponse<UserResponse>> GetUser(ClaimsPrincipal user);
    public Task<ApiResponse> InsertUser(UserRequest request);
    public Task<ApiResponse> InsertAdmin(UserRequest request);
    public Task<ApiResponse> Update(string userName, UserRequest request);
    public Task<ApiResponse> Delete(string userName);
    public Task<ApiResponse<LoyaltyPointsResponse>> GetLoyaltyPoints(ClaimsPrincipal user);
    public Task<ApiResponse> UnlockUser(string userName);
    public Task<ApiResponse<List<UserResponse>>> GetAll();
    public Task<ApiResponse<UserResponse>> GetByUserName(string userName);
}

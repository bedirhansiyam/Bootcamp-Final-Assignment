using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApi.Base;
using WebApi.Data;
using WebApi.Schema;

namespace WebApi.Operation;

public class UserService : IUserService
{
    private readonly UserManager<User> userManager;
    private readonly IMapper mapper;

    public UserService(UserManager<User> userManager, IMapper mapper)
    {
        this.userManager = userManager;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> Delete(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            return new ApiResponse("Invalid user name");
        try
        {
            var user = userManager.Users.Where(x => x.UserName == userName).FirstOrDefault();
            await userManager.DeleteAsync(user);

            return new ApiResponse("The user has been successfully deleted", true);
        }
        catch(Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }

    public async Task<ApiResponse<List<UserResponse>>> GetAll()
    {
        try
        {
            var userList = userManager.Users.Include(IncludeType.UserToCoupon).ToList();
            var mapped = mapper.Map<List<UserResponse>>(userList);

            return new ApiResponse<List<UserResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<UserResponse>>(ex.Message);
        }
    }

    public async Task<ApiResponse<UserResponse>> GetByUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            return new ApiResponse<UserResponse>("User name was null");

        try
        {
            var user = userManager.Users.Where(x => x.UserName == userName).Include(IncludeType.UserToCoupon).FirstOrDefault();
            var mapped = mapper.Map<UserResponse>(user);

            return new ApiResponse<UserResponse>(mapped);
        }
        catch( Exception ex)
        {
            return new ApiResponse<UserResponse>(ex.Message);
        }
    }

    public async Task<ApiResponse<LoyaltyPointsResponse>> GetLoyaltyPoints(ClaimsPrincipal user)
    {
        try
        {
            var userId = userManager.GetUserId(user);
            var authUser = await userManager.Users.SingleAsync(x => x.Id == userId);
            var mapped = mapper.Map<LoyaltyPointsResponse>(authUser);

            return new ApiResponse<LoyaltyPointsResponse>(mapped);
        }
        catch(Exception ex)
        {
            return new ApiResponse<LoyaltyPointsResponse>(ex.Message);
        }
    }

    public async Task<ApiResponse<UserResponse>> GetUser(ClaimsPrincipal user)
    {
        try
        {
            var userId = userManager.GetUserId(user);
            var authUser = await userManager.Users.Include(IncludeType.UserToCoupon).SingleAsync(x => x.Id == userId);
            var mapped = mapper.Map<UserResponse>(authUser);

            return new ApiResponse<UserResponse>(mapped);
        }
        catch(Exception ex)
        {
            return new ApiResponse<UserResponse>(ex.Message);
        }
    }

    public async Task<ApiResponse> InsertAdmin(UserRequest request)
    {
        if (request is null)
            return new ApiResponse("Request was null");

        if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Surname))
            return new ApiResponse("All fields must be filled");

        try
        {
            var user = mapper.Map<User>(request);
            user.EmailConfirmed = true;
            user.TwoFactorEnabled = false;
            user.Role = Role.Admin;
            user.LockoutEnabled = false;

            var response = await userManager.CreateAsync(user, request.Password);
            if (!response.Succeeded)
                return new ApiResponse(response.Errors.FirstOrDefault()?.Description);

            await userManager.AddToRoleAsync(user, Role.Admin);

            return new ApiResponse("The admin has been successfully created", true);
        }
        catch (Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }

    public async Task<ApiResponse> InsertUser(UserRequest request)
    {
        if (request is null)
            return new ApiResponse("Request was null");

        if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Surname))
            return new ApiResponse("All fields must be filled");

        try
        {
            var user = mapper.Map<User>(request);
            user.EmailConfirmed = true;
            user.TwoFactorEnabled = false;
            user.Role = Role.User;

            var response = await userManager.CreateAsync(user, request.Password);
            if (!response.Succeeded)
                return new ApiResponse(response.Errors.FirstOrDefault()?.Description);

            await userManager.AddToRoleAsync(user, Role.User);

            return new ApiResponse("The user has been successfully created", true);
        }
        catch(Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }

    public async Task<ApiResponse> UnlockUser(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            return new ApiResponse("Invalid user name");

        var exist = userManager.Users.Where(x => x.UserName == userName).FirstOrDefault();
        if (exist is null)
            return new ApiResponse("User not found");

        try
        {
            var user = userManager.Users.Where(x => x.UserName == userName).FirstOrDefault();
            await userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow);
            await userManager.UpdateAsync(user);

            return new ApiResponse("The account has been successfully unlocked", true);
        }
        catch(Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }

    public async Task<ApiResponse> Update(string userId, UserRequest request)
    {
        if (request is null)
            return new ApiResponse("Request was null");

        if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Surname))
            return new ApiResponse("All fields must be filled");

        var exist = userManager.Users.Where(x => x.Id == userId).AsNoTracking().FirstOrDefault();
        if (exist is null)
            return new ApiResponse("User not found");

        try
        {
            var mapped = mapper.Map<User>(request);
            mapped.Id = userId;
            await userManager.UpdateAsync(mapped);

            return new ApiResponse("The user has been successfully updated", true);
        }
        catch(Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }
}

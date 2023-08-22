using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Base;
using WebApi.Data;
using WebApi.Schema;

namespace WebApi.Operation;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly JwtConfig jwtConfig;
    private readonly IMapper mapper;

    public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager, IOptionsMonitor<JwtConfig> jwtConfig, IMapper mapper)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.jwtConfig = jwtConfig.CurrentValue;
        this.mapper = mapper;
    }

    public async Task<ApiResponse> ChangePassword(ClaimsPrincipal user, ChangePasswordRequest request)
    {
        if (request is null)
            return new ApiResponse("Request was null");

        if (string.IsNullOrWhiteSpace(request.OldPassword))
            return new ApiResponse("Password cannot be empty");

        if (string.IsNullOrWhiteSpace(request.NewPassword))
            return new ApiResponse("New password cannot be empty");

        var authUser = await userManager.GetUserAsync(user);
        var response = await userManager.ChangePasswordAsync(authUser, request.OldPassword, request.NewPassword);

        if (!response.Succeeded)
            return new ApiResponse("Password could not be changed");

        return new ApiResponse("The password has been successfully changed", true);
    }

    public async Task<ApiResponse<TokenResponse>> SignIn(TokenRequest request)
    {
        if (request is null)
            return new ApiResponse<TokenResponse>("Request was null");

        if (string.IsNullOrWhiteSpace(request.UserName))
            return new ApiResponse<TokenResponse>("Username cannot be empty");

        if (string.IsNullOrWhiteSpace(request.Password))
            return new ApiResponse<TokenResponse>("Password cannot be empty");

        var loginResult = await signInManager.PasswordSignInAsync(request.UserName, request.Password, true, true);

        if(loginResult.IsLockedOut)
            return new ApiResponse<TokenResponse>("Your account has been locked out. Please contact admin to unlock");

        if (!loginResult.Succeeded)
            return new ApiResponse<TokenResponse>("You have entered an invalid username or password");

        var user = await userManager.FindByNameAsync(request.UserName);

        var token = Token(user);
        TokenResponse tokenResponse = new()
        {
            AccessToken = token,
            ExpireTime = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            UserName = user.UserName,
        };
        return new ApiResponse<TokenResponse>(tokenResponse);
    }

    public async Task<ApiResponse> Insert(UserRequest request)
    {
        if (request is null)
            return new ApiResponse("Request was null");

        if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Surname))
            return new ApiResponse("All fields must be filled");

        var entity = mapper.Map<User>(request);
        entity.EmailConfirmed = true;
        entity.TwoFactorEnabled = false;
        entity.Role = Role.User;

        var responseUser = await userManager.CreateAsync(entity, request.Password);
        if (!responseUser.Succeeded)
            return new ApiResponse(responseUser.Errors.FirstOrDefault()?.Description);

        await userManager.AddToRoleAsync(entity, Role.User);

        return new ApiResponse("Your account has been successfully created", true);
    }

    public async Task<ApiResponse> SignOut()
    {
        await signInManager.SignOutAsync();
        return new ApiResponse("You have successfully sign-out", true);
    }

    private string Token(User user)
    {
        Claim[] claims = GetClaims(user);
        var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

        var jwtToken = new JwtSecurityToken(
            jwtConfig.Issuer,
            jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return accessToken;
    }

    private Claim[] GetClaims(User user)
    {
        var claims = new[]
        {
            new Claim("Id", user.Id),
            new Claim("Name",user.Name),
            new Claim("Surname",user.Surname),
            new Claim("UserName",user.UserName),
            new Claim("Email",user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Role, user.Role)
        };
        return claims;
    }


}

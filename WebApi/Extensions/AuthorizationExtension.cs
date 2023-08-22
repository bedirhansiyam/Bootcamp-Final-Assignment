using WebApi.Base;

namespace WebApi.Service;

public static class AuthorizationExtension
{
    public static void AddAuthorizationExtension(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policy.Admin, policy =>
                policy.RequireRole(Role.Admin));

            options.AddPolicy(Policy.AdminAndUser, policy =>
                policy.RequireRole(Role.User, Role.Admin));

            options.AddPolicy(Policy.User, policy =>
                policy.RequireRole(Role.User));
        });
    }
}

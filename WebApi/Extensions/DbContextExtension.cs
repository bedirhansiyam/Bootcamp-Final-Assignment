using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Data.Context;

namespace WebApi.Service.Extensions;

public static class DbContextExtension
{
    public static void AddDbContextExtension(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConfig = configuration.GetConnectionString("MsSqlConnection");
        services.AddDbContext<WebEfDbContext>(opts =>
        {
            opts.UseSqlServer(dbConfig);
        });

        services.AddIdentity<User, IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<WebEfDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(365);
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
        });
    }
}

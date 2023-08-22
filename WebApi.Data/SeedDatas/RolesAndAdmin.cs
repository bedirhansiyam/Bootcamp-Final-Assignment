using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Base;

namespace WebApi.Data;

public static class RolesAndAdmin
{
    public static void Seed(this ModelBuilder builder)
    {
        string adminId = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
        string roleId = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";

        List<IdentityRole> roles = new()
        {
            new IdentityRole { Id = roleId, Name = Role.Admin, NormalizedName = Role.Admin },
            new IdentityRole { Name = Role.User, NormalizedName = Role.User }

        };

        builder.Entity<IdentityRole>().HasData(roles);


        User admin = new()
        {
            Id = adminId,
            Name = "admin",
            Surname = "admin",
            Email = "admin@mail.com",
            NormalizedEmail = "ADMIN@MAIL.COM",
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Role = "admin",
            EmailConfirmed = true,
        };

        PasswordHasher<User> hashed = new PasswordHasher<User>();
        admin.PasswordHash = hashed.HashPassword(admin, "admin123");

        builder.Entity<User>().HasData(admin);


        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = roleId,
            UserId = adminId,
        });
    }
}

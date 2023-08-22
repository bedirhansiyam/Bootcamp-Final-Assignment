using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Data;

[Table("Users", Schema = "dbo")]
public class User : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Role { get; set; }
    public decimal LoyaltyPoints { get; set; }

    public virtual ICollection<Coupon> Coupons { get; set; }

    public virtual ICollection<Basket> BasketItems { get; set; }
    public virtual ICollection<Order> Orders { get; set; }

    [NotMapped]
    public string FullName
    {
        get { return Name + " " + Surname; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(20);
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(20);
            builder.Property(x => x.Surname).IsRequired(true).HasMaxLength(20);
            builder.Property(x => x.Email).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.PasswordHash).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.LoyaltyPoints).IsRequired(true).HasPrecision(17, 2).HasDefaultValue(0);

            builder.HasIndex(x => x.UserName).IsUnique(true);

        }
    }
}

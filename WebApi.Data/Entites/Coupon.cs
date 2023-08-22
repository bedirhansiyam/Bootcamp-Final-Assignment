using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Base;

namespace WebApi.Data;

[Table("Coupons", Schema = "dbo")]
public class Coupon : BaseEntity
{
    public string Code { get; set; }
    public DateTime DueDate { get; set; }
    public decimal Amount { get; set; }
    public bool IsUsed { get; set; }
    public string UserName { get; set; }

    public virtual User User { get; set; }
}

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Ignore(x => x.CreatedAt);
        builder.Ignore(x => x.CreatedBy);
        builder.Ignore(x => x.UpdatedAt);
        builder.Ignore(x => x.UpdatedBy);

        builder.Property(x => x.Code).IsRequired(true).HasMaxLength(7);
        builder.Property(x => x.DueDate).IsRequired(true);
        builder.Property(x => x.Amount).IsRequired(true).HasPrecision(17, 2);
        builder.Property(x => x.IsUsed).IsRequired(true).HasDefaultValue(false);
        builder.Property(x => x.UserName).IsRequired(false).HasMaxLength(256);

        builder.HasIndex(x => x.Code).IsUnique(true);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Coupons)
            .HasForeignKey(x => x.UserName)
            .HasPrincipalKey(x => x.UserName)
            .IsRequired(false);



    }
}

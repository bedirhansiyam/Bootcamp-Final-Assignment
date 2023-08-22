using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Base;

namespace WebApi.Data;

[Table("Orders", Schema = "dbo")]
public class Order : BaseEntity
{
    public string OrderNumber { get; set; }
    public string UserName { get; set; }
    public DateTime OrderedDate { get; set; }
    public string Address { get; set; }
    public decimal TotalAmount { get; set; }
    public string CouponCode { get; set; }
    public decimal? CouponAmount { get; set; }
    public decimal? UserLoyaltyPoints { get; set; }
    public decimal Payment { get; set; }
    public decimal? LoyaltyPointsToBeEarned { get; set; }
    public string Status { get; set; }
    public string PaymentReferenceNumber { get; set; }


    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    public virtual User User { get; set; }
}

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Ignore(x => x.CreatedAt);
        builder.Ignore(x => x.CreatedBy);
        builder.Ignore(x => x.UpdatedAt);
        builder.Ignore(x => x.UpdatedBy);

        builder.Property(x => x.OrderNumber).IsRequired(true).HasMaxLength(9);
        builder.Property(x => x.Address).IsRequired(true).HasMaxLength(250);
        builder.Property(x => x.OrderedDate).IsRequired(true);
        builder.Property(x => x.TotalAmount).IsRequired(true).HasPrecision(17, 2);
        builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(256);
        builder.Property(x => x.CouponAmount).IsRequired(false).HasPrecision(17, 2);
        builder.Property(x => x.CouponCode).IsRequired(false).HasMaxLength(7);
        builder.Property(x => x.UserLoyaltyPoints).IsRequired(false).HasPrecision(17, 2);
        builder.Property(x => x.Payment).IsRequired(true).HasPrecision(17, 2);
        builder.Property(x => x.LoyaltyPointsToBeEarned).IsRequired(true).HasPrecision(17, 2);
        builder.Property(x => x.Status).IsRequired(true).HasMaxLength(15);
        builder.Property(x => x.PaymentReferenceNumber).IsRequired(false).HasMaxLength(12);


        builder.HasIndex(x => x.OrderNumber).IsUnique(true);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.UserName)
            .HasPrincipalKey(x => x.UserName);
    }
}

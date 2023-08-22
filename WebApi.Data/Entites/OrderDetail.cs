using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Base;

namespace WebApi.Data;

[Table("OrderDetails", Schema = "dbo")]
public class OrderDetail : BaseEntity
{
    public string OrderNumber { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public string UserName { get; set; }

    public virtual Product Product { get; set; }
    public virtual Order Order { get; set; }
}

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Ignore(x => x.CreatedAt);
        builder.Ignore(x => x.CreatedBy);
        builder.Ignore(x => x.UpdatedAt);
        builder.Ignore(x => x.UpdatedBy);

        builder.Property(x => x.OrderNumber).IsRequired(true).HasMaxLength(9);
        builder.Property(x => x.Quantity).IsRequired(true);
        builder.Property(x => x.ProductId).IsRequired(true);
        builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(256);

        builder.HasOne(x => x.Order)
            .WithMany(x => x.OrderDetails)
            .HasForeignKey(x => x.OrderNumber)
            .HasPrincipalKey(x => x.OrderNumber)
            .IsRequired(true);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Base;

namespace WebApi.Data;

[Table("Baskets", Schema = "dbo")]
public class Basket : BaseEntity
{
    public int Quantity { get; set; }
    public string UserId { get; set; }
    public int ProductId { get; set; }

    public virtual User User { get; set; }
    public virtual Product Product{ get; set; }
}

public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn(); 
        builder.Property(x => x.CreatedAt).IsRequired(false);
        builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(30);
        builder.Ignore(x => x.UpdatedAt);
        builder.Ignore(x => x.UpdatedBy);

        builder.Property(x => x.Quantity).IsRequired(true);
        builder.Property(x => x.UserId).IsRequired(true);
        builder.Property(x => x.ProductId).IsRequired(true);

        builder.HasOne(x => x.User)
            .WithMany(x => x.BasketItems)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Product)
            .WithMany(x => x.Baskets)
            .HasForeignKey(x => x.ProductId);
    }
}

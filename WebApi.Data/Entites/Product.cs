using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApi.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Data;

[Table("Products", Schema = "dbo")]
public class Product : BaseEntity
{
    public string Name { get; set; }
    public string  Description { get; set; }
    public string ProducerCompany { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public decimal Price { get; set; }
    public int? Stock { get; set; }
    public decimal MaxPointsEarned { get; set; }
    public decimal PercentageOfPoints { get; set; }
    public bool IsActive { get; set; }  

    public virtual ICollection<ProductCategory> Categories { get; set; }

    public virtual ICollection<Basket> Baskets { get; set; }
}

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.CreatedAt).IsRequired(false);
        builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(30);
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false).HasMaxLength(30);

        builder.Property(x => x.Name).IsRequired(true).HasMaxLength(30);
        builder.Property(x => x.Description).IsRequired(false).HasMaxLength(200);
        builder.Property(x => x.Price).IsRequired(true).HasPrecision(17,2);
        builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);
        builder.Property(x => x.ProducerCompany).IsRequired(false).HasMaxLength(40);
        builder.Property(x => x.PercentageOfPoints).IsRequired(true).HasPrecision(17, 2).HasDefaultValue(0);
        builder.Property(x => x.ReleaseDate).IsRequired(false);
        builder.Property(x => x.MaxPointsEarned).IsRequired(true).HasPrecision(17, 2).HasDefaultValue(0);
        builder.Property(x => x.Stock).IsRequired(false).HasDefaultValue(0);


        builder.HasIndex(x => x.Name).IsUnique(true);
    }
}

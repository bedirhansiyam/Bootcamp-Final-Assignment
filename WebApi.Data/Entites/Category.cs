using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Base;

namespace WebApi.Data;

[Table("Categories", Schema = "dbo")]
public class Category : BaseEntity
{
    public string Name { get; set; }
    public string Tag { get; set; }
    public string Url { get; set; }

    public virtual ICollection<ProductCategory> Products { get; set; }
}

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.CreatedAt).IsRequired(false);
        builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(30);
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false).HasMaxLength(30);

        builder.Property(x => x.Name).IsRequired(true).HasMaxLength(25);
        builder.Property(x => x.Tag).IsRequired(false).HasMaxLength(70);
        builder.Property(x => x.Url).IsRequired(false).HasMaxLength(30);

        builder.HasIndex(x => x.Name).IsUnique(true);
    }
}

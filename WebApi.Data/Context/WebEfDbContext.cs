using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApi.Base;
using static WebApi.Data.User;

namespace WebApi.Data.Context;

public class WebEfDbContext : IdentityDbContext<User>
{
    public WebEfDbContext(DbContextOptions<WebEfDbContext> options) : base(options)
    {
        
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CouponConfiguration());
        modelBuilder.ApplyConfiguration(new BasketConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());

        modelBuilder.Seed();
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        AddTimeStamps();
        return base.SaveChanges();
    }

    private void AddTimeStamps()
    {
        var entities = ChangeTracker.Entries();

        foreach (var entity in entities)
        {
            if(entity.Entity is BaseEntity trackable)
            {
                switch(entity.State)
                {
                    case EntityState.Modified:
                        trackable.UpdatedAt = DateTime.UtcNow;
                        trackable.UpdatedBy = "admin";
                        if(!(entity.Entity is Coupon || entity.Entity is ProductCategory || entity.Entity is Order || entity.Entity is OrderDetail))
                        {
                            entity.Property("CreatedAt").IsModified = false;
                            entity.Property("CreatedBy").IsModified = false;
                        }
                        break;

                    case EntityState.Added:
                        trackable.CreatedAt = DateTime.UtcNow;
                        trackable.CreatedBy = "admin";
                        break;
                }
            }
        }
    }
}

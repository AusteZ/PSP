using Microsoft.EntityFrameworkCore;
using PSP.Models.Entities.RelationalTables;

namespace PSP.Models.Entities;

public class PSPDatabaseContext : DbContext
{
    public PSPDatabaseContext(DbContextOptions<PSPDatabaseContext> options)
    : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Service> Services { get; set; } = null!;
    public DbSet<ServiceSlot> ServiceSlots { get; set; } = null!;
    public DbSet<Cancellation> Cancellations { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<OrderProduct> OrderProducts { get; set; } = null!;
    public DbSet<Coupon> Coupons { get; set; } = null!;
    public DbSet<Discount> Discounts { get; set; } = null!;
    public DbSet<ProductDiscount> ProductsDiscounts { get; set; } = null!;
    public DbSet<ServiceDiscount> ServicesDiscounts { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                Id = 1,
                Username = "test",
                Password = "123",
                Role = "none"
            },
            new Customer
            {
                Id = 2,
                Username = "admin",
                Password = "admin",
                Role = "admin"
            }
        );

        modelBuilder.Entity<Service>()
            .HasMany(p => p.ServiceSlots)
            .WithOne(d => d.Service)
            .HasForeignKey(d => d.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderProduct>()
            .HasKey(op => new { op.ProductId, op.OrderId });
        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Product)
            .WithMany(p => p.Orders)
            .HasForeignKey(op => op.ProductId);
        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Order)
            .WithMany(p => p.Products)
            .HasForeignKey(op => op.OrderId);

        modelBuilder.Entity<ServiceSlot>()
            .HasOne(ss => ss.Order)
            .WithMany(o => o.ServiceSlots)
            .HasForeignKey(ss => ss.OrderId);

        modelBuilder.Entity<ProductDiscount>()
            .HasKey(pd => new { pd.ProductId, pd.DiscountId });
        modelBuilder.Entity<ProductDiscount>()
            .HasOne(pd => pd.Discount)
            .WithMany(d => d.Products)
            .HasForeignKey(pd => pd.DiscountId);
        modelBuilder.Entity<ProductDiscount>()
            .HasOne(pd => pd.Product)
            .WithMany(d => d.Discounts)
            .HasForeignKey(pd => pd.ProductId);

        modelBuilder.Entity<ServiceDiscount>()
            .HasKey(pd => new { pd.ServiceId, pd.DiscountId });
        modelBuilder.Entity<ServiceDiscount>()
            .HasOne(pd => pd.Discount)
            .WithMany(d => d.Services)
            .HasForeignKey(pd => pd.DiscountId);
        modelBuilder.Entity<ServiceDiscount>()
            .HasOne(pd => pd.Service)
            .WithMany(d => d.Discounts)
            .HasForeignKey(pd => pd.ServiceId);
    }
}

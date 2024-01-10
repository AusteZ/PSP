using Microsoft.EntityFrameworkCore;
using PSP.Services;

namespace PSP.Models.Entities;

public class PSPDatabaseContext : DbContext
{
    public PSPDatabaseContext(DbContextOptions<PSPDatabaseContext> options)
    : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Service> Services { get; set; } = null!;
    public DbSet<ServiceSlot> ServiceSlots { get; set; } = null!;
    public DbSet<Cancellation> Cancellations { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<OrderProduct> OrderProducts { get; set; } = null!;
    public DbSet<OrderService> OrderServices { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        modelBuilder.Entity<OrderService>()
            .HasKey(os => new { ServiceId = os.ServiceSlotId, os.OrderId });
        modelBuilder.Entity<OrderService>()
            .HasOne(os => os.ServiceSlot)
            .WithMany(p => p.Orders)
            .HasForeignKey(os => os.ServiceSlotId);
        modelBuilder.Entity<OrderService>()
            .HasOne(os => os.Order)
            .WithMany(p => p.ServiceSlots)
            .HasForeignKey(os => os.OrderId);
    }
}

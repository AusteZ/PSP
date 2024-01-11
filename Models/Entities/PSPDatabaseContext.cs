using Microsoft.EntityFrameworkCore;
using PSP.Models.Entities.RelationalTables;

namespace PSP.Models.Entities;

public class PSPDatabaseContext : DbContext
{
    public PSPDatabaseContext(DbContextOptions<PSPDatabaseContext> options)
    : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        Database.EnsureCreated();
    }

    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Service> Services { get; set; } = null!;
    public DbSet<ServiceSlot> ServiceSlots { get; set; } = null!;
    public DbSet<Cancellation> Cancellations { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<OrderProduct> OrderProducts { get; set; } = null!;


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
    }
}

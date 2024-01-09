using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace PSP.Models.Entities;

public class PSPDatabaseContext : DbContext
{
    public PSPDatabaseContext(DbContextOptions<PSPDatabaseContext> options)
    : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Service> Services { get; set; } = null!;
    public DbSet<ServiceSlot> ServiceSlots { get; set; } = null!;
    public DbSet<Cancellation> Cancellations { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<OrderProducts> OrderProducts { get; set; } = null!;
    public DbSet<OrderServices> OrderServices { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer) 
            .WithMany(c => c.Orders) 
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        //modelBuilder.Entity<OrderProducts>()
        //    .HasKey(op => new { op.OrderId, op.ProductId });

        //modelBuilder.Entity<OrderProducts>()
        //    .HasOne(op => op.Order)
        //    .WithMany(o => o.OrderProducts)
        //    .HasForeignKey(op => op.OrderId)
        //    .OnDelete(DeleteBehavior.Cascade);

        //modelBuilder.Entity<OrderProducts>()
        //    .HasOne(op => op.Product)
        //    .WithMany()
        //    .HasForeignKey(op => op.ProductId)
        //    .OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<OrderServices>()
        //    .HasKey(os => new { os.OrderId, os.ServiceId });

        //modelBuilder.Entity<OrderServices>()
        //    .HasOne(os => os.Order)
        //    .WithMany(o => o.OrderServices)
        //    .HasForeignKey(os => os.OrderId)
        //    .OnDelete(DeleteBehavior.Cascade)
        //     ;

        //modelBuilder.Entity<OrderServices>()
        //    .HasOne(os => os.Service)
        //    .WithMany(o => o.OrderServices)
        //    .HasForeignKey(os => os.ServiceId)
        //    .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Order>()
           .HasMany(e => e.Products)
           .WithMany(e => e.Orders)
           .UsingEntity<OrderProducts>(
               l => l.HasOne<Product>(e => e.Product).WithMany(e => e.OrderProducts).HasForeignKey(e => e.ProductId),
               r => r.HasOne<Order>(e => e.Order).WithMany(e => e.OrderProducts).HasForeignKey(e => e.OrderId));

        modelBuilder.Entity<Order>()
           .HasMany(e => e.Services)
           .WithMany(e => e.Orders)
           .UsingEntity<OrderServices>(
               l => l.HasOne<Service>(e => e.Service).WithMany(e => e.OrderServices).HasForeignKey(e => e.ServiceId),
               r => r.HasOne<Order>(e => e.Order).WithMany(e => e.OrderServices).HasForeignKey(e => e.OrderId));
    }

}

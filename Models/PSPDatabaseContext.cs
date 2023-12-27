using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PSP.Models;

public class PSPDatabaseContext : DbContext
{
    public PSPDatabaseContext(DbContextOptions<PSPDatabaseContext> options)
    : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; } = null!;
}
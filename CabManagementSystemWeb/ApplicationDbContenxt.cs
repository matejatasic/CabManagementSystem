using Microsoft.EntityFrameworkCore;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branch>()
            .HasOne(b => b.Manager)
            .WithOne()
            .HasForeignKey<Branch>(b => b.ManagerId);
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<Client> Clients { get; set; }
}

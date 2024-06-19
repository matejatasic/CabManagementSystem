using Microsoft.EntityFrameworkCore;
using CabManagementSystemWeb.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CabManagementSystemWeb;

public sealed class ApplicationDbContext : IdentityDbContext<User, Role, int>
{
    public ApplicationDbContext(DbContextOptions options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Branch>()
            .HasOne(b => b.Manager)
            .WithOne()
            .HasForeignKey<Branch>(b => b.ManagerId);
    }

    // public DbSet<User> Users { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Route> Routes { get; set; }
    // public DbSet<Role> Roles { get; set; }
}

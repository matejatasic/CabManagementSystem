using Microsoft.EntityFrameworkCore;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options): base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
}

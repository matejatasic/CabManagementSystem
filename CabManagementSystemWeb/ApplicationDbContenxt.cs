using Microsoft.EntityFrameworkCore;

namespace CabManagementSystemWeb;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options): base(options)
    {
    }
}

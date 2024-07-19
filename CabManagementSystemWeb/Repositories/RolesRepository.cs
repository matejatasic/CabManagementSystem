using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace CabManagementSystemWeb.Repositories;

public class RolesRepository : IRepository<Role>
{
    private readonly ApplicationDbContext _dbContext;

    public RolesRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Role>> GetAll()
    {
        List<Role> roles = await _dbContext.Roles.ToListAsync();

        return roles;
    }

    public async Task<Role?> GetById(int id)
    {
        Role? roles = await _dbContext.Roles.FindAsync(id);
        _dbContext.ChangeTracker.Clear();

        return roles;
    }

    public async Task<Role?> GetBy(string property, object value)
    {
        DbSet<Role> roles = _dbContext.Roles;
        IQueryable<Role> query = roles;

        if (property == "name")
        {
            query = roles.Where(r => r.Name == value);
        }

        Role? role = await query.FirstOrDefaultAsync();

        return role;
    }

    public async Task<Role> Create(Role role)
    {
        _dbContext.Add(role);
        await _dbContext.SaveChangesAsync();

        return role;
    }

    public async Task<Role> Update(Role role)
    {
        _dbContext.Roles.Update(role);
        await _dbContext.SaveChangesAsync();

        return role;
    }

    public async Task<Role> Delete(Role role)
    {
        _dbContext.Roles.Remove(role);
        await _dbContext.SaveChangesAsync();

        return role;
    }
}
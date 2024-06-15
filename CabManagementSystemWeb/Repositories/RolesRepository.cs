using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace CabManagementSystemWeb.Repositories;

public class RolesRepository : IRepository<Role, RoleCreateDto, RoleDetailDto>
{
    private readonly ApplicationDbContext _dbContext;

    public RolesRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<RoleDetailDto>> GetAll()
    {
        List<Role> roles = await _dbContext.Roles.ToListAsync();

        return roles.Select(r => r.ConvertToDetailDto()).ToList();
    }

    public async Task<RoleDetailDto?> GetById(int id)
    {
        Role? roles = await _dbContext.Roles.FindAsync(id);
        _dbContext.ChangeTracker.Clear();

        return roles?.ConvertToDetailDto();
    }

    public async Task<RoleDetailDto?> GetBy(string property, object value)
    {
        DbSet<Role> roles = _dbContext.Roles;
        IQueryable<Role> query = roles;

        if (property == "name")
        {
            query = roles.Where(r => r.Name == value);
        }

        Role? role = await query.FirstOrDefaultAsync();

        return role?.ConvertToDetailDto();
    }

    public async Task<RoleDetailDto> Create(RoleCreateDto roleDto)
    {
        Role role = roleDto.ConvertToEntity();

        role.Created = DateTime.UtcNow;
        _dbContext.Add(role);
        await _dbContext.SaveChangesAsync();

        return role.ConvertToDetailDto();
    }

    public async Task<RoleDetailDto> Update(Role role)
    {
        role.Updated = DateTime.UtcNow;
        _dbContext.Roles.Update(role);
        await _dbContext.SaveChangesAsync();

        return role.ConvertToDetailDto();
    }

    public async Task<RoleDetailDto> Delete(Role role)
    {
        _dbContext.Roles.Remove(role);
        await _dbContext.SaveChangesAsync();

        return role.ConvertToDetailDto();
    }
}
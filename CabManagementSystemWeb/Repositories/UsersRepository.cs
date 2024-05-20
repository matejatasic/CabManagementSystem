using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace CabManagementSystemWeb.Repositories;

public class UsersRepository : IRepository<User, UserCreateDto, UserDetailDto>
{
    private readonly ApplicationDbContext _dbContext;

    public UsersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<UserDetailDto>> GetAll()
    {
        List<User> users = await _dbContext.Users.ToListAsync();

        return users.Select(u => u.ConvertToDetailDto()).ToList();
    }

    public async Task<UserDetailDto?> GetById(int id)
    {
        User? users = await _dbContext.Users.FindAsync(id);
        _dbContext.ChangeTracker.Clear();

        return users?.ConvertToDetailDto();
    }

    public async Task<UserDetailDto> Create(UserCreateDto userDto)
    {
        User user = userDto.ConvertToEntity();

        user.Created = DateTime.UtcNow;
        _dbContext.Add(user);
        await _dbContext.SaveChangesAsync();

        return user.ConvertToDetailDto();
    }

    public async Task<UserDetailDto> Update(User user)
    {
        user.Updated = DateTime.UtcNow;
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();

        return user.ConvertToDetailDto();
    }

    public async Task<UserDetailDto> Delete(User user)
    {
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();

        return user.ConvertToDetailDto();
    }
}
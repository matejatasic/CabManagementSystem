using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace CabManagementSystemWeb.Repositories;

public class UsersRepository : IRepository<User>
{
    private readonly ApplicationDbContext _dbContext;

    public UsersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<User>> GetAll()
    {
        List<User> users = await _dbContext.Users.ToListAsync();

        return users;
    }

    public async Task<User?> GetById(int id)
    {
        User? user = await _dbContext.Users.FindAsync(id);
        _dbContext.ChangeTracker.Clear();

        return user;
    }

    public async Task<User?> GetBy(string property, object value)
    {
        DbSet<User> users = _dbContext.Users;
        IQueryable<User> query = users;

        if (property == "username")
        {
            query = users.Where(u => u.UserName == value);
        }
        else if (property == "email")
        {
            query = users.Where(u => u.Email == value);
        }

        User? user = await query.FirstOrDefaultAsync();

        return user;
    }

    public async Task<User> Create(User user)
    {
        _dbContext.Add(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<User> Update(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<User> Delete(User user)
    {
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }
}
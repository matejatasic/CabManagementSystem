using CabManagementSystemWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace CabManagementSystemWeb.Repositories;

public class RoutesRepository : IRepository<Route>
{
    private readonly ApplicationDbContext _dbContext;

    public RoutesRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Route>> GetAll()
    {
        List<Route> routes = await _dbContext.Routes.ToListAsync();

        return routes;
    }

    public async Task<Route?> GetById(int id)
    {
        Route? route = await _dbContext.Routes.FindAsync(id);
        _dbContext.ChangeTracker.Clear();

        return route;
    }

    public async Task<Route?> GetBy(string property, object value)
    {
        return null;
    }

    public async Task<Route> Create(Route route)
    {
        _dbContext.Add(route);
        await _dbContext.SaveChangesAsync();

        return route;
    }

    public async Task<Route> Update(Route route)
    {
        _dbContext.Routes.Update(route);
        await _dbContext.SaveChangesAsync();

        return route;
    }

    public async Task<Route> Delete(Route route)
    {
        _dbContext.Routes.Remove(route);
        await _dbContext.SaveChangesAsync();

        return route;
    }
}

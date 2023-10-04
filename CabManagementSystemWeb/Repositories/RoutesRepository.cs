using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace CabManagementSystemWeb.Repositories;

public class RoutesRepository : IRepository<Route, RouteCreateDto, RouteDetailDto>
{
    private readonly ApplicationDbContext _dbContext;

    public RoutesRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<RouteDetailDto>> GetAll()
    {
        List<Route> routes = await _dbContext.Routes.ToListAsync();

        return routes.Select(r => r.ConvertToDetailDto()).ToList();
    }

    public async Task<RouteDetailDto?> GetById(int id)
    {
        Route? route = await _dbContext.Routes.FindAsync(id);
        _dbContext.ChangeTracker.Clear();

        return route?.ConvertToDetailDto();
    }

    public async Task<RouteDetailDto> Create(RouteCreateDto routeDto)
    {
        Route route = routeDto.ConvertToEntity();

        route.Created = DateTime.UtcNow;
        _dbContext.Add(route);
        await _dbContext.SaveChangesAsync();

        return route.ConvertToDetailDto();
    }

    public async Task<RouteDetailDto> Update(Route route)
    {
        route.Updated = DateTime.UtcNow;

        _dbContext.Routes.Update(route);
        await _dbContext.SaveChangesAsync();

        return route.ConvertToDetailDto();
    }

    public async Task<RouteDetailDto> Delete(Route route)
    {
        _dbContext.Routes.Remove(route);
        await _dbContext.SaveChangesAsync();

        return route.ConvertToDetailDto();
    }
}
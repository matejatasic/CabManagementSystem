using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Services;

public class RoutesService : IRoutesService
{
    private readonly IRepository<Route> _repository;
    private readonly IRepository<Employee> _employeesRepository;
    private readonly IRepository<User> _usersRepository;

    public RoutesService(
        IRepository<Route> repository,
        IRepository<Employee> employeesRepository,
        IRepository<User> usersRepository
    )
    {
        _repository = repository;
        _employeesRepository = employeesRepository;
        _usersRepository = usersRepository;
    }

    public async Task<IEnumerable<RouteDetailDto>> GetAll()
    {
        List<Route> route = await _repository.GetAll();

        return route.Select(r => r.ConvertToDetailDto()).ToList();
    }

    public async Task<RouteDetailDto?> GetById(int id)
    {
        Route? route = await _repository.GetById(id);

        if (route == null)
        {
            throw new NotFoundException();
        }

        return route.ConvertToDetailDto();
    }

    public async Task<RouteDetailDto> Create(RouteCreateDto routeCreateDto)
    {
        if (await GetDriverById(routeCreateDto.DriverId) == null)
        {
            throw new NotFoundException($"The driver with the id {routeCreateDto.DriverId} does not exist");
        }

        if (await GetTravelerById(routeCreateDto.TravelerId) == null)
        {
            throw new NotFoundException($"The traveler with the id {routeCreateDto.TravelerId} does not exist");
        }

        Route route = routeCreateDto.ConvertToEntity();
        route.Created = DateTime.UtcNow;
        Route newRoute = await _repository.Create(route);

        return newRoute.ConvertToDetailDto();
    }

    public async Task<RouteDetailDto> Update(int id, RouteUpdateDto routeUpdateDto)
    {
        Route? route = await _repository.GetById(id);

        if (route == null)
        {
            throw new NotFoundException($"The route with id {id} does not exist");
        }

        if (await GetDriverById(route.DriverId) == null)
        {
            throw new NotFoundException($"The driver with id {routeUpdateDto.DriverId} does not exist");
        }

        if (await GetTravelerById(route.TravelerId) == null)
        {
            throw new NotFoundException($"The traveler with the id {routeUpdateDto.TravelerId} does not exist");
        }

        route = ChangeUpdatedValues(route, routeUpdateDto);
        route.Updated = DateTime.UtcNow;
        route = await _repository.Update(route);

        return route.ConvertToDetailDto();
    }

    private Route ChangeUpdatedValues(Route route, RouteUpdateDto routeUpdateDto)
    {
        if (routeUpdateDto.FromAddress != null)
        {
            route.FromAddress = routeUpdateDto.FromAddress;
        }

        if (routeUpdateDto.ToAddress != null)
        {
            route.ToAddress = routeUpdateDto.ToAddress;
        }

        if (routeUpdateDto.TravelCost != null)
        {
            route.TravelCost = routeUpdateDto.TravelCost;
        }

        if (routeUpdateDto.TravelerId != null)
        {
            route.TravelerId = (int)routeUpdateDto.TravelerId;
        }

        if (routeUpdateDto.DriverId != null)
        {
            route.DriverId = (int)routeUpdateDto.DriverId;
        }

        return route;
    }

    private async Task<Employee?> GetDriverById(int id)
    {
        return await _employeesRepository.GetById(id);
    }

    private async Task<User?> GetTravelerById(int id)
    {
        return await _usersRepository.GetById(id);
    }

    public async Task<RouteDetailDto> Delete(int id)
    {
        Route? route = await _repository.GetById(id);

        if (route == null)
        {
            throw new NotFoundException($"The route with id {id} does not exist");
        }

        await _repository.Delete(route);

        return route.ConvertToDetailDto();
    }
}

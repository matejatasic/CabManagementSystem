using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Services;

public class RoutesService : IRoutesService
{
    private readonly IRepository<Route,RouteCreateDto, RouteDetailDto> _repository;
    private readonly IRepository<Employee, EmployeeCreateDto, EmployeeDetailDto> _employeesRepository;
    private readonly IHashService _hashService;

    public RoutesService(
        IRepository<Route,RouteCreateDto, RouteDetailDto> repository,
        IRepository<Employee, EmployeeCreateDto, EmployeeDetailDto> employeesRepository
    )
    {
        _repository = repository;
        _employeesRepository = employeesRepository;
    }

    public async Task<IEnumerable<RouteDetailDto>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<RouteDetailDto?> GetById(int id)
    {
        RouteDetailDto? routeDetailDto = await _repository.GetById(id);

        if (routeDetailDto == null)
        {
            throw new NotFoundException();
        }

        return routeDetailDto;
    }

    public async Task<RouteDetailDto> Create(RouteCreateDto routeCreateDto)
    {
        if (await GetDriverById(routeCreateDto.DriverId) == null)
        {
            throw new NotFoundException($"The driver with the id {routeCreateDto.DriverId} does not exist");
        }

        return await _repository.Create(routeCreateDto);
    }

    public async Task<RouteDetailDto> Update(int id, RouteUpdateDto routeUpdateDto)
    {
        RouteDetailDto? routeDetailDto = await _repository.GetById(id);

        if (routeDetailDto == null)
        {
            throw new NotFoundException($"The route with id {id} does not exist");
        }

        if (await GetDriverById(routeDetailDto.DriverId) == null)
        {
            throw new NotFoundException($"The driver with id {routeUpdateDto.DriverId} does not exist");
        }

        routeDetailDto = await _repository.Update(routeUpdateDto.ConvertToEntity(id));

        return routeDetailDto;
    }

    private async Task<EmployeeDetailDto?> GetDriverById(int id)
    {
        return await _employeesRepository.GetById(id);
    }

    public async Task<RouteDetailDto> Delete(int id)
    {
        RouteDetailDto? routeDetailDto = await _repository.GetById(id);

        if (routeDetailDto == null)
        {
            throw new NotFoundException($"The route with id {id} does not exist");
        }

        await _repository.Delete(routeDetailDto.ConvertToEntity());

        return routeDetailDto;
    }
}
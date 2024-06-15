using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Services;

public class CarsService : ICarsService
{
    private readonly IRepository<Car, CarCreateDto, CarDetailDto> _repository;
    private readonly IRepository<Employee, EmployeeCreateDto, EmployeeDetailDto> _employeesRepository;

    public CarsService(
        IRepository<Car, CarCreateDto, CarDetailDto> repository,
        IRepository<Employee, EmployeeCreateDto, EmployeeDetailDto> employeesRepository
    )
    {
        _repository = repository;
        _employeesRepository = employeesRepository;
    }

    public async Task<IEnumerable<CarDetailDto>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<CarDetailDto?> GetById(int id)
    {
        CarDetailDto? carDetailDto = await _repository.GetById(id);

        if (carDetailDto == null)
        {
            throw new NotFoundException();
        }

        return carDetailDto;
    }

    public async Task<CarDetailDto> Create(CarCreateDto carCreateDto)
    {
        if (await GetDriverById(carCreateDto.DriverId) == null)
        {
            throw new NotFoundException($"The driver with the id {carCreateDto.DriverId} does not exist");
        }

        return await _repository.Create(carCreateDto);
    }

    public async Task<CarDetailDto> Update(int id, CarUpdateDto carUpdateDto)
    {
        CarDetailDto? carDetailDto = await _repository.GetById(id);

        if (carDetailDto == null)
        {
            throw new NotFoundException($"The car with id {id} does not exist");
        }

        if (await GetDriverById(carDetailDto.DriverId) == null)
        {
            throw new NotFoundException($"The driver with id {carUpdateDto.DriverId} does not exist");
        }

        carDetailDto = await _repository.Update(carUpdateDto.ConvertToEntity(id));

        return carDetailDto;
    }

    private async Task<EmployeeDetailDto?> GetDriverById(int id)
    {
        return await _employeesRepository.GetById(id);
    }

    public async Task<CarDetailDto> Delete(int id)
    {
        CarDetailDto? carDetailDto = await _repository.GetById(id);

        if (carDetailDto == null)
        {
            throw new NotFoundException($"The car with id {id} does not exist");
        }

        await _repository.Delete(carDetailDto.ConvertToEntity());

        return carDetailDto;
    }
}
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Services;

public class CarsService : ICarsService
{
    private readonly IRepository<Car> _repository;
    private readonly IRepository<Employee> _employeesRepository;

    public CarsService(
        IRepository<Car> repository,
        IRepository<Employee> employeesRepository
    )
    {
        _repository = repository;
        _employeesRepository = employeesRepository;
    }

    public async Task<IEnumerable<CarDetailDto>> GetAll()
    {
        List<Car> cars = await _repository.GetAll();

        return cars.Select(c => c.ConvertToDetailDto()).ToList();
    }

    public async Task<CarDetailDto?> GetById(int id)
    {
        Car? car = await _repository.GetById(id);

        if (car == null)
        {
            throw new NotFoundException();
        }

        return car.ConvertToDetailDto();
    }

    public async Task<CarDetailDto> Create(CarCreateDto carCreateDto)
    {
        if (await GetDriverById(carCreateDto.DriverId) == null)
        {
            throw new NotFoundException($"The driver with the id {carCreateDto.DriverId} does not exist");
        }

        Car car = carCreateDto.ConvertToEntity();
        car.Created = DateTime.UtcNow;
        Car newCar = await _repository.Create(car);

        return newCar.ConvertToDetailDto();
    }

    public async Task<CarDetailDto> Update(int id, CarUpdateDto carUpdateDto)
    {
        Car? car = await _repository.GetById(id);

        if (car == null)
        {
            throw new NotFoundException($"The car with id {id} does not exist");
        }

        if (await GetDriverById(car.DriverId) == null)
        {
            throw new NotFoundException($"The driver with id {carUpdateDto.DriverId} does not exist");
        }

        car = ChangeUpdatedValues(car, carUpdateDto);
        car.Updated = DateTime.UtcNow;
        car = await _repository.Update(car);

        return car.ConvertToDetailDto();
    }

    private Car ChangeUpdatedValues(Car car, CarUpdateDto carUpdateDto)
    {
        if (carUpdateDto.Name != null)
        {
            car.Name = carUpdateDto.Name;
        }

        if (carUpdateDto.NumberOfSeats != null)
        {
            car.NumberOfSeats = (int)carUpdateDto.NumberOfSeats;
        }

        if (carUpdateDto.FuelType != null)
        {
            car.FuelType = carUpdateDto.FuelType;
        }

        if (carUpdateDto.RegisteredUntil != null)
        {
            car.RegisteredUntil = carUpdateDto.RegisteredUntil;
        }

        if (carUpdateDto.RegistrationPlates != null)
        {
            car.RegistrationPlates = carUpdateDto.RegistrationPlates;
        }

        if (carUpdateDto.DriverId != null)
        {
            car.DriverId = (int)carUpdateDto.DriverId;
        }

        return car;
    }

    private async Task<Employee?> GetDriverById(int id)
    {
        return await _employeesRepository.GetById(id);
    }

    public async Task<CarDetailDto> Delete(int id)
    {
        Car? car = await _repository.GetById(id);

        if (car == null)
        {
            throw new NotFoundException($"The car with id {id} does not exist");
        }

        await _repository.Delete(car);

        return car.ConvertToDetailDto();
    }
}
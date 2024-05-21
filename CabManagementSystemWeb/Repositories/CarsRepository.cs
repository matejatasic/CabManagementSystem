using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CabManagementSystemWeb.Repositories;

public class CarsRepository : IRepository<Car, CarCreateDto, CarDetailDto>
{
    private readonly ApplicationDbContext _dbContext;

    public CarsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CarDetailDto>> GetAll()
    {
        List<Car> cars = await _dbContext.Cars.ToListAsync();

        return cars.Select(c => c.ConvertToDetailDto()).ToList();
    }

    public async Task<CarDetailDto?> GetById(int id)
    {
        Car? car = await _dbContext.Cars.FindAsync(id);
        _dbContext.ChangeTracker.Clear();

        return car?.ConvertToDetailDto();
    }

    public async Task<CarDetailDto> Create(CarCreateDto carDto)
    {
        Car car = carDto.ConvertToEntity();

        car.Created = DateTime.UtcNow;
        _dbContext.Add(car);
        await _dbContext.SaveChangesAsync();

        return car.ConvertToDetailDto();
    }

    public async Task<CarDetailDto> Update(Car car)
    {
        car.Updated = DateTime.UtcNow;

        _dbContext.Cars.Update(car);
        await _dbContext.SaveChangesAsync();

        return car.ConvertToDetailDto();
    }

    public async Task<CarDetailDto> Delete(Car car)
    {
        _dbContext.Cars.Remove(car);
        await _dbContext.SaveChangesAsync();

        return car.ConvertToDetailDto();
    }
}
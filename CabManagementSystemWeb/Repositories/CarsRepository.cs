using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CabManagementSystemWeb.Repositories;

public class CarsRepository : IRepository<Car>
{
    private readonly ApplicationDbContext _dbContext;

    public CarsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Car>> GetAll()
    {
        List<Car> cars = await _dbContext.Cars.ToListAsync();

        return cars;
    }

    public async Task<Car?> GetById(int id)
    {
        Car? car = await _dbContext.Cars.FindAsync(id);
        _dbContext.ChangeTracker.Clear();

        return car;
    }
    public async Task<Car?> GetBy(string property, object value)
    {
        return null;
    }

    public async Task<Car> Create(Car car)
    {
        _dbContext.Add(car);
        await _dbContext.SaveChangesAsync();

        return car;
    }

    public async Task<Car> Update(Car car)
    {
        _dbContext.Cars.Update(car);
        await _dbContext.SaveChangesAsync();

        return car;
    }

    public async Task<Car> Delete(Car car)
    {
        _dbContext.Cars.Remove(car);
        await _dbContext.SaveChangesAsync();

        return car;
    }
}
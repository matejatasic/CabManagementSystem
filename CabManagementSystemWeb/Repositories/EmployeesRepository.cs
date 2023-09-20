using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace CabManagementSystemWeb.Repositories;

public class EmployeesRepository : IRepository<Employee>
{
    private readonly ApplicationDbContext _dbContext;

    public EmployeesRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Employee>> GetAll()
    {
        return await _dbContext.Employees.ToListAsync();
    }

    public async Task<Employee?> GetById(int id)
    {
        Employee? employees = await _dbContext.Employees.FindAsync(id);
        _dbContext.ChangeTracker.Clear();

        return employees;
    }

    public async Task<Employee> Create(Employee employee)
    {
        employee.Created = DateTime.UtcNow;
        _dbContext.Add(employee);
        await _dbContext.SaveChangesAsync();

        return employee;
    }

    public async Task<Employee> Update(Employee employee)
    {
        _dbContext.ChangeTracker.Clear();

        employee.Updated = DateTime.UtcNow;
        _dbContext.Employees.Update(employee);
        await _dbContext.SaveChangesAsync();

        return employee;
    }

    public async Task<Employee> Delete(Employee employee)
    {
        _dbContext.Employees.Remove(employee);
        await _dbContext.SaveChangesAsync();

        return employee;
    }
}
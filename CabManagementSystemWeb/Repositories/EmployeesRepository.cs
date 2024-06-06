using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace CabManagementSystemWeb.Repositories;

public class EmployeesRepository : IRepository<Employee, EmployeeCreateDto, EmployeeDetailDto>
{
    private readonly ApplicationDbContext _dbContext;

    public EmployeesRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<EmployeeDetailDto>> GetAll()
    {
        List<Employee> employees = await _dbContext.Employees.ToListAsync();

        return employees.Select(e => e.ConvertToDetailDto()).ToList();
    }

    public async Task<EmployeeDetailDto?> GetById(int id)
    {
        Employee? employees = await _dbContext.Employees.FindAsync(id);
        _dbContext.ChangeTracker.Clear();

        return employees?.ConvertToDetailDto();
    }

    public async Task<EmployeeDetailDto?> GetBy(string property, object value)
    {
        return null;
    }

    public async Task<EmployeeDetailDto> Create(EmployeeCreateDto employeeDto)
    {
        Employee employee = employeeDto.ConvertToEntity();

        employee.Created = DateTime.UtcNow;
        _dbContext.Add(employee);
        await _dbContext.SaveChangesAsync();

        return employee.ConvertToDetailDto();
    }

    public async Task<EmployeeDetailDto> Update(Employee employee)
    {
        employee.Updated = DateTime.UtcNow;
        _dbContext.Employees.Update(employee);
        await _dbContext.SaveChangesAsync();

        return employee.ConvertToDetailDto();
    }

    public async Task<EmployeeDetailDto> Delete(Employee employee)
    {
        _dbContext.Employees.Remove(employee);
        await _dbContext.SaveChangesAsync();

        return employee.ConvertToDetailDto();
    }
}
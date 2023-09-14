using Microsoft.EntityFrameworkCore;
using CabManagementSystemWeb.Entities;
using Microsoft.AspNetCore.Mvc;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Contracts;

namespace CabManagementSystemWeb.Services;

public class EmployeesService : IEmployeesService
{
    private readonly IRepository<Employee> _repository;

    public EmployeesService(IRepository<Employee> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Employee>> GetAll()
    {
        return await _repository.GetAll();
    }

    public Task<Employee?> GetById(int id)
    {
        return _repository.GetById(id);
    }

    public Task<Employee> Create(Employee employee)
    {
        return _repository.Create(employee);
    }

    public async Task<Employee?> Update(int id, Employee newEmployee)
    {
        Employee? employee = await _repository.GetById(id);

        if (employee == null)
        {
            return null;
        }

        newEmployee.Id = id;

        await _repository.Update(newEmployee);

        return newEmployee;
    }

    public async Task<Employee?> Delete(int id)
    {
        Employee? response = await _repository.Delete(id);

        return response;
    }
}
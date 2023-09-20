using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;

namespace CabManagementSystemWeb.Services;

public class EmployeesService : IEmployeesService
{
    private readonly IRepository<Employee> _repository;
    private readonly IRepository<Branch> _branchRepository;

    public EmployeesService(
        IRepository<Employee> repository,
        IRepository<Branch> branchRepository
    )
    {
        _repository = repository;
        _branchRepository = branchRepository;
    }

    public async Task<IEnumerable<Employee>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Employee?> GetById(int id)
    {
        Employee? employee = await _repository.GetById(id);

        if (employee == null)
        {
            throw new NotFoundException();
        }

        return employee;
    }

    public async Task<Employee> Create(Employee employee)
    {
        Branch? branch = await _branchRepository.GetById(employee.BranchId);

        if (branch == null)
        {
            throw new NotFoundException($"The branch with the id {employee.BranchId} does not exist");
        }

        return await _repository.Create(employee);
    }

    public async Task<Employee> Update(int id, Employee newEmployee)
    {
        Employee? employee = await _repository.GetById(id);

        if (employee == null)
        {
            throw new NotFoundException($"The employee with id {id} does not exist");
        }

        newEmployee.Id = id;

        await _repository.Update(newEmployee);

        return employee;
    }

    public async Task<Employee> Delete(int id)
    {
        Employee? employee = await _repository.GetById(id);

        if (employee == null)
        {
            throw new NotFoundException($"The employee with id {id} does not exist");
        }

        await _repository.Delete(employee);

        return employee;
    }
}
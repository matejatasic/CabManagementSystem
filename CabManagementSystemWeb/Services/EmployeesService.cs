using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Services;

public class EmployeesService : IEmployeesService
{
    private readonly IRepository<Employee> _repository;
    private readonly IRepository<Branch> _branchesRepository;
    private readonly IRepository<User> _usersRepository;

    public EmployeesService(
        IRepository<Employee> repository,
        IRepository<Branch> branchesRepository,
        IRepository<User> usersRepository
    )
    {
        _repository = repository;
        _branchesRepository = branchesRepository;
        _usersRepository = usersRepository;
    }

    public async Task<IEnumerable<EmployeeDetailDto>> GetAll()
    {
        List<Employee> employees = await _repository.GetAll();

        return employees.Select(e => e.ConvertToDetailDto()).ToList();
    }

    public async Task<EmployeeDetailDto?> GetById(int id)
    {
        Employee? employee = await _repository.GetById(id);

        if (employee == null)
        {
            throw new NotFoundException();
        }

        return employee.ConvertToDetailDto();
    }

    public async Task<EmployeeDetailDto> Create(EmployeeCreateDto employeeCreateDto)
    {
        if (
            await GetBranchById(employeeCreateDto.BranchId) == null
        )
        {
            throw new NotFoundException($"The branch with the id {employeeCreateDto.BranchId} does not exist");
        }

        if (await GetUserById(employeeCreateDto.UserId) == null)
        {
            throw new NotFoundException($"The user with the id {employeeCreateDto.UserId} does not exist");
        }

        Employee employee = employeeCreateDto.ConvertToEntity();
        employee.Created = DateTime.UtcNow;
        Employee newEmployee = await _repository.Create(employee);

        return newEmployee.ConvertToDetailDto();
    }

    public async Task<EmployeeDetailDto> Update(int id, EmployeeUpdateDto employeeUpdateDto)
    {
        Employee? employee = await _repository.GetById(id);

        if (employee == null)
        {
            throw new NotFoundException($"The employee with id {id} does not exist");
        }

        if (employeeUpdateDto.BranchId != null && await GetBranchById(employee.BranchId) == null)
        {
            throw new NotFoundException($"The branch with id {employeeUpdateDto.BranchId} does not exist");
        }

        if (employeeUpdateDto.UserId != null && await GetUserById(employee.UserId) == null)
        {
            throw new NotFoundException($"The user with the id {employeeUpdateDto.UserId} does not exist");
        }

        employee = ChangeUpdatedValues(employee, employeeUpdateDto);
        employee.Updated = DateTime.UtcNow;
        employee = await _repository.Update(employee);

        return employee.ConvertToDetailDto();
    }

    private Employee ChangeUpdatedValues(Employee employee, EmployeeUpdateDto employeeUpdateDto)
    {
        if (employeeUpdateDto.UserId != null)
        {
            employee.UserId = (int)employeeUpdateDto.UserId;
        }
        if (employeeUpdateDto.BranchId != null)
        {
            employee.BranchId = (int)employeeUpdateDto.BranchId;
        }

        return employee;
    }

    private async Task<Branch?> GetBranchById(int id)
    {
        return await _branchesRepository.GetById(id);
    }

    private async Task<User?> GetUserById(int id)
    {
        return await _usersRepository.GetById(id);
    }

    public async Task<EmployeeDetailDto> Delete(int id)
    {
        Employee? employee = await _repository.GetById(id);

        if (employee == null)
        {
            throw new NotFoundException($"The employee with id {id} does not exist");
        }

        await _repository.Delete(employee);

        return employee.ConvertToDetailDto();
    }
}
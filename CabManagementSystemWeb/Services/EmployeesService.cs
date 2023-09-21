using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Services;

public class EmployeesService : IEmployeesService
{
    private readonly IRepository<Employee, EmployeeCreateDto, EmployeeDetailDto> _repository;
    private readonly IRepository<Branch, BranchCreateDto, BranchDetailDto> _branchRepository;

    public EmployeesService(
        IRepository<Employee, EmployeeCreateDto, EmployeeDetailDto> repository,
        IRepository<Branch, BranchCreateDto, BranchDetailDto> branchRepository
    )
    {
        _repository = repository;
        _branchRepository = branchRepository;
    }

    public async Task<IEnumerable<EmployeeDetailDto>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<EmployeeDetailDto?> GetById(int id)
    {
        EmployeeDetailDto? employeeDetailDto = await _repository.GetById(id);

        if (employeeDetailDto == null)
        {
            throw new NotFoundException();
        }

        return employeeDetailDto;
    }

    public async Task<EmployeeDetailDto> Create(EmployeeCreateDto employeeCreateDto)
    {
        BranchDetailDto? branchDetailDto = await _branchRepository.GetById(employeeCreateDto.BranchId);

        if (branchDetailDto == null)
        {
            throw new NotFoundException($"The branch with the id {employeeCreateDto.BranchId} does not exist");
        }

        return await _repository.Create(employeeCreateDto);
    }

    public async Task<EmployeeDetailDto> Update(int id, EmployeeUpdateDto employeeUpdateDto)
    {
        EmployeeDetailDto? employeeDetailDto = await _repository.GetById(id);

        if (employeeDetailDto == null)
        {
            throw new NotFoundException($"The employee with id {id} does not exist");
        }

        employeeDetailDto = await _repository.Update(employeeUpdateDto.ConvertToEntity(id));

        return employeeDetailDto;
    }

    public async Task<EmployeeDetailDto> Delete(int id)
    {
        EmployeeDetailDto? employeeDetailDto = await _repository.GetById(id);

        if (employeeDetailDto == null)
        {
            throw new NotFoundException($"The employee with id {id} does not exist");
        }

        await _repository.Delete(employeeDetailDto.ConvertToEntity());

        return employeeDetailDto;
    }
}
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Services;

public class EmployeesService : IEmployeesService
{
    private readonly IRepository<Employee, EmployeeCreateDto, EmployeeDetailDto> _repository;
    private readonly IRepository<Branch, BranchCreateDto, BranchDetailDto> _branchesRepository;
    private readonly IRepository<User, UserCreateDto, UserDetailDto> _usersRepository;

    public EmployeesService(
        IRepository<Employee, EmployeeCreateDto, EmployeeDetailDto> repository,
        IRepository<Branch, BranchCreateDto, BranchDetailDto> branchesRepository,
        IRepository<User, UserCreateDto, UserDetailDto> usersRepository
    )
    {
        _repository = repository;
        _branchesRepository = branchesRepository;
        _usersRepository = usersRepository;
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

        return await _repository.Create(employeeCreateDto);
    }

    public async Task<EmployeeDetailDto> Update(int id, EmployeeUpdateDto employeeUpdateDto)
    {
        EmployeeDetailDto? employeeDetailDto = await _repository.GetById(id);

        if (employeeDetailDto == null)
        {
            throw new NotFoundException($"The employee with id {id} does not exist");
        }

        if (employeeUpdateDto.BranchId != null && await GetBranchById(employeeDetailDto.BranchId) == null)
        {
            throw new NotFoundException($"The branch with id {employeeUpdateDto.BranchId} does not exist");
        }

        if (employeeUpdateDto.UserId != null && await GetUserById(employeeDetailDto.UserId) == null)
        {
            throw new NotFoundException($"The user with the id {employeeUpdateDto.UserId} does not exist");
        }

        employeeDetailDto = await _repository.Update(employeeUpdateDto.ConvertToEntity(id));

        return employeeDetailDto;
    }

    private async Task<BranchDetailDto?> GetBranchById(int id)
    {
        return await _branchesRepository.GetById(id);
    }

    private async Task<UserDetailDto?> GetUserById(int id)
    {
        return await _usersRepository.GetById(id);
    }

    public async Task<EmployeeDetailDto> Delete(int id)
    {
        EmployeeDetailDto? employeeDetailDto = await _repository.GetById(id);

        if (employeeDetailDto == null)
        {
            throw new NotFoundException($"The employee with id {id} does not exist");
        }

        await _repository.Delete(employeeDetailDto.ConvertToEntity(id));

        return employeeDetailDto;
    }
}
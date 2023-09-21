using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Services;

public class BranchesService : IBranchesService
{
    private readonly IRepository<Branch, BranchCreateDto, BranchDetailDto> _repository;
    private readonly IRepository<Employee, EmployeeCreateDto, EmployeeDetailDto> _employeesRepository;

    public BranchesService(
        IRepository<Branch, BranchCreateDto, BranchDetailDto> repository,
        IRepository<Employee, EmployeeCreateDto, EmployeeDetailDto> employeesRepository
    )
    {
        _repository = repository;
        _employeesRepository = employeesRepository;
    }

    public async Task<IEnumerable<BranchDetailDto>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<BranchDetailDto?> GetById(int id)
    {
        BranchDetailDto? branchDetailDto = await _repository.GetById(id);

        if (branchDetailDto == null)
        {
            throw new NotFoundException();
        }

        return branchDetailDto;
    }

    public async Task<BranchDetailDto> Create(BranchCreateDto branchCreateDto)
    {
        return await _repository.Create(branchCreateDto);
    }

    public async Task<BranchDetailDto> Update(int id, BranchUpdateDto branchUpdateDto)
    {
        BranchDetailDto? branchDetailDto = await _repository.GetById(id);

        if (branchDetailDto == null)
        {
            throw new NotFoundException($"The branch with id {id} does not exist");
        }

        branchDetailDto = await _repository.Update(branchUpdateDto.ConvertToEntity(id));

        return branchDetailDto;
    }

    public async Task<BranchDetailDto> Delete(int id)
    {
        BranchDetailDto? branchDetailDto = await _repository.GetById(id);

        if (branchDetailDto == null)
        {
            throw new NotFoundException($"The branch with id {id} does not exist");
        }

        await _repository.Delete(branchDetailDto.ConvertToEntity());

        return branchDetailDto;
    }
}
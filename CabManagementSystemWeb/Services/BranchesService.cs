using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Services;

public class BranchesService : IBranchesService
{
    private readonly IRepository<Branch> _repository;
    private readonly IRepository<Employee> _employeesRepository;

    public BranchesService(
        IRepository<Branch> repository,
        IRepository<Employee> employeesRepository
    )
    {
        _repository = repository;
        _employeesRepository = employeesRepository;
    }

    public async Task<IEnumerable<BranchDetailDto>> GetAll()
    {
        List<Branch> branches = await _repository.GetAll();
        return branches.Select(b => b.ConvertToDetailDto()).ToList();
    }

    public async Task<BranchDetailDto?> GetById(int id)
    {
        Branch? branch = await _repository.GetById(id);

        if (branch == null)
        {
            throw new NotFoundException();
        }

        return branch.ConvertToDetailDto();
    }

    public async Task<BranchDetailDto> Create(BranchCreateDto branchCreateDto)
    {
        if (
            branchCreateDto.ManagerId != null
            && await GetEmployeeById((int)branchCreateDto.ManagerId) == null
        )
        {
            throw new NotFoundException($"The employee with the id {branchCreateDto.ManagerId} does not exist");
        }

        Branch branch = branchCreateDto.ConvertToEntity();
        branch.Created = DateTime.UtcNow;
        Branch newBranch = await _repository.Create(branch);

        return newBranch.ConvertToDetailDto();
    }

    public async Task<BranchDetailDto> Update(int id, BranchUpdateDto branchUpdateDto)
    {
        Branch? branch = await _repository.GetById(id);

        if (branch == null)
        {
            throw new NotFoundException($"The branch with id {id} does not exist");
        }

        if (
            branchUpdateDto.ManagerId != null
            && await GetEmployeeById((int)branchUpdateDto.ManagerId) == null
        )
        {
            throw new NotFoundException($"The employee with the id {branchUpdateDto.ManagerId} does not exist");
        }

        branch = ChangeUpdatedValues(branch, branchUpdateDto);
        branch.Updated = DateTime.UtcNow;
        branch = await _repository.Update(branch);

        return branch.ConvertToDetailDto();
    }

    private Branch ChangeUpdatedValues(Branch branch, BranchUpdateDto branchUpdateDto)
    {
        if (branchUpdateDto.Name != null)
        {
            branch.Name = branchUpdateDto.Name;
        }

        if (branchUpdateDto.ManagerId != null)
        {
            branch.ManagerId = branchUpdateDto.ManagerId;
        }

        return branch;
    }

    private async Task<Employee?> GetEmployeeById(int id)
    {
        return await _employeesRepository.GetById(id);
    }

    public async Task<BranchDetailDto> Delete(int id)
    {
        Branch? branch = await _repository.GetById(id);

        if (branch == null)
        {
            throw new NotFoundException($"The branch with id {id} does not exist");
        }

        await _repository.Delete(branch);

        return branch.ConvertToDetailDto();
    }
}
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;

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

    public async Task<IEnumerable<Branch>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Branch?> GetById(int id)
    {
        Branch? branch = await _repository.GetById(id);

        if (branch == null)
        {
            throw new NotFoundException();
        }

        return branch;
    }

    public async Task<Branch> Create(Branch branch)
    {
        return await _repository.Create(branch);
    }

    public async Task<Branch> Update(int id, Branch newBranch)
    {
        Branch? branch = await _repository.GetById(id);

        if (branch == null)
        {
            throw new NotFoundException($"The branch with id {id} does not exist");
        }

        newBranch.Id = id;

        await _repository.Update(newBranch);

        return branch;
    }

    public async Task<Branch> Delete(int id)
    {
        Branch? branch = await _repository.GetById(id);

        if (branch == null)
        {
            throw new NotFoundException($"The branch with id {id} does not exist");
        }

        await _repository.Delete(branch);

        return branch;
    }
}
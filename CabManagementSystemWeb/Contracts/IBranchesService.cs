using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Contracts;

public interface IBranchesService
{
    public Task<IEnumerable<Branch>> GetAll();

    public Task<Branch?> GetById(int id);

    public Task<Branch> Create(Branch branch);

    public Task<Branch> Update(int id, Branch branch);

    public Task<Branch> Delete(int id);
}
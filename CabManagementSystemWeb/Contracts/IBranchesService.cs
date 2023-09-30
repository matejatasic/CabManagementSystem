using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Contracts;

public interface IBranchesService
{
    public Task<IEnumerable<BranchDetailDto>> GetAll();

    public Task<BranchDetailDto?> GetById(int id);

    public Task<BranchDetailDto> Create(BranchCreateDto branchCreateDto);

    public Task<BranchDetailDto> Update(int id, BranchUpdateDto branchUpdateDto);

    public Task<BranchDetailDto> Delete(int id);
}
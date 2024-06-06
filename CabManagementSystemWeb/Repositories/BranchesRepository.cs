using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace CabManagementSystemWeb.Repositories;

public class BranchesRepository : IRepository<Branch, BranchCreateDto, BranchDetailDto>
{
    private readonly ApplicationDbContext _dbContext;

    public BranchesRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<BranchDetailDto>> GetAll()
    {
        List<Branch> branches = await _dbContext.Branches.ToListAsync();

        return branches.Select(b => b.ConvertToDetailDto()).ToList();
    }

    public async Task<BranchDetailDto?> GetById(int id)
    {
        Branch? branch = await _dbContext.Branches.FindAsync(id);
        _dbContext.ChangeTracker.Clear();

        return branch?.ConvertToDetailDto();
    }

    public async Task<BranchDetailDto?> GetBy(string property, object value)
    {
        return null;
    }

    public async Task<BranchDetailDto> Create(BranchCreateDto branchDto)
    {
        Branch branch = branchDto.ConvertToEntity();

        branch.Created = DateTime.UtcNow;
        _dbContext.Branches.Add(branch);
        await _dbContext.SaveChangesAsync();

        return branch.ConvertToDetailDto();
    }

    public async Task<BranchDetailDto> Update(Branch branch)
    {
        branch.Updated = DateTime.UtcNow;

        _dbContext.Branches.Update(branch);
        await _dbContext.SaveChangesAsync();

        return branch.ConvertToDetailDto();
    }

    public async Task<BranchDetailDto> Delete(Branch branch)
    {
        _dbContext.Branches.Remove(branch);
        await _dbContext.SaveChangesAsync();

        return branch.ConvertToDetailDto();
    }
}
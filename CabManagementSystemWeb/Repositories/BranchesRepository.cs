using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace CabManagementSystemWeb.Repositories;

public class BranchesRepository : IRepository<Branch>
{
    private readonly ApplicationDbContext _dbContext;

    public BranchesRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Branch>> GetAll()
    {
        List<Branch> branches = await _dbContext.Branches.ToListAsync();

        return branches;
    }

    public async Task<Branch?> GetById(int id)
    {
        Branch? branch = await _dbContext.Branches.FindAsync(id);
        _dbContext.ChangeTracker.Clear();

        return branch;
    }

    public async Task<Branch?> GetBy(string property, object value)
    {
        return null;
    }

    public async Task<Branch> Create(Branch branch)
    {
        _dbContext.Branches.Add(branch);
        await _dbContext.SaveChangesAsync();

        return branch;
    }

    public async Task<Branch> Update(Branch branch)
    {
        _dbContext.Branches.Update(branch);
        await _dbContext.SaveChangesAsync();

        return branch;
    }

    public async Task<Branch> Delete(Branch branch)
    {
        _dbContext.Branches.Remove(branch);
        await _dbContext.SaveChangesAsync();

        return branch;
    }
}
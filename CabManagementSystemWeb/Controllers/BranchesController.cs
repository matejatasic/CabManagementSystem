using Microsoft.AspNetCore.Mvc;
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;

namespace CabManagementSystemWeb.Controllers;

[Route("api/branches")]
[ApiController]
public class BranchesController : ControllerBase
{
    private readonly IBranchesService _branchesService;

    public BranchesController(IBranchesService branchesService)
    {
        _branchesService = branchesService;
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<Branch>>> GetAll()
    {
        IEnumerable<Branch> branches = await _branchesService.GetAll();
        return new JsonResult(branches);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Branch>> GetById(int id)
    {
        Branch? branch = await _branchesService.GetById(id);

        if (branch == null)
        {
            return NotFound();
        }

        return new JsonResult(branch);
    }

    [HttpPost("")]
    public async Task<ActionResult<Branch>> Create(Branch branch)
    {
        try {
            branch = await _branchesService.Create(branch);

            return CreatedAtAction(nameof(GetAll), new { id = branch.Id }, branch);
        }
        catch(NotFoundException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Branch branch)
    {
        try
        {
            await _branchesService.Update(id, branch);

            return NoContent();
        }
        catch(NotFoundException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _branchesService.Delete(id);

            return NoContent();
        }
        catch(NotFoundException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
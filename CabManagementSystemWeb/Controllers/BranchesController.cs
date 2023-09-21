using Microsoft.AspNetCore.Mvc;
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Dtos;

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
    public async Task<ActionResult<IEnumerable<BranchDetailDto>>> GetAll()
    {
        IEnumerable<BranchDetailDto> branches = await _branchesService.GetAll();
        return new JsonResult(branches);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BranchDetailDto>> GetById(int id)
    {
        BranchDetailDto? branchDetailDto = await _branchesService.GetById(id);

        if (branchDetailDto == null)
        {
            return NotFound();
        }

        return new JsonResult(branchDetailDto);
    }

    [HttpPost("")]
    public async Task<ActionResult<Branch>> Create(BranchCreateDto branchCreateDto)
    {
        try {
            BranchDetailDto branchDetailDto = await _branchesService.Create(branchCreateDto);

            return CreatedAtAction(nameof(GetAll), new { id = branchDetailDto.Id }, branchDetailDto);
        }
        catch(NotFoundException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, BranchUpdateDto branchUpdateDto)
    {
        try
        {
            await _branchesService.Update(id, branchUpdateDto);

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
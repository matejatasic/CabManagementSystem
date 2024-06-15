using Microsoft.AspNetCore.Mvc;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Controllers;

[Route("api/roles")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRolesService _rolesService;

    public RolesController(IRolesService rolesService)
    {
        _rolesService = rolesService;
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<RoleDetailDto>>> GetAll()
    {
        IEnumerable<RoleDetailDto> roles = await _rolesService.GetAll();

        return new JsonResult(roles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoleDetailDto>> GetById(int id)
    {
        try
        {
            RoleDetailDto? roleDetailDto = await _rolesService.GetById(id);

            return new JsonResult(roleDetailDto);
        }
        catch(NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("")]
    public async Task<ActionResult<RoleDetailDto>> Create(RoleCreateDto roleCreateDto)
    {
        try {
            RoleDetailDto roleDetailDto = await _rolesService.Create(roleCreateDto);

            return CreatedAtAction(nameof(GetAll), new { id = roleDetailDto.Id }, roleDetailDto);
        }
        catch(NotFoundException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, RoleUpdateDto roleUpdateDto)
    {
        try
        {
            await _rolesService.Update(id, roleUpdateDto);

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
            await _rolesService.Delete(id);

            return NoContent();
        }
        catch(NotFoundException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
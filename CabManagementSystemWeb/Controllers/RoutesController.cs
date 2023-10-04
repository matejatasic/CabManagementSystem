using Microsoft.AspNetCore.Mvc;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Controllers;

[Route("api/routes")]
[ApiController]
public class RoutesController : ControllerBase
{
    private readonly IRoutesService _routesService;

    public RoutesController(IRoutesService routesService)
    {
        _routesService = routesService;
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<RouteDetailDto>>> GetAll()
    {
        IEnumerable<RouteDetailDto> employees = await _routesService.GetAll();

        return new JsonResult(employees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RouteDetailDto>> GetById(int id)
    {
        try
        {
            RouteDetailDto? employeeDetailDto = await _routesService.GetById(id);

            return new JsonResult(employeeDetailDto);
        }
        catch(NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("")]
    public async Task<ActionResult<RouteDetailDto>> Create(RouteCreateDto employeeCreateDto)
    {
        try {
            RouteDetailDto employeeDetailDto = await _routesService.Create(employeeCreateDto);

            return CreatedAtAction(nameof(GetAll), new { id = employeeDetailDto.Id }, employeeDetailDto);
        }
        catch(NotFoundException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, RouteUpdateDto employeeUpdateDto)
    {
        try
        {
            await _routesService.Update(id, employeeUpdateDto);

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
            await _routesService.Delete(id);

            return NoContent();
        }
        catch(NotFoundException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
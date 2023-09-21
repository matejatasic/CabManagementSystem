using Microsoft.AspNetCore.Mvc;
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Controllers;

[Route("api/employees")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeesService _employeesService;

    public EmployeesController(IEmployeesService employeesService)
    {
        _employeesService = employeesService;
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<EmployeeDetailDto>>> GetAll()
    {
        IEnumerable<EmployeeDetailDto> employees = await _employeesService.GetAll();

        return new JsonResult(employees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDetailDto>> GetById(int id)
    {
        try
        {
            EmployeeDetailDto? employeeDetailDto = await _employeesService.GetById(id);

            return new JsonResult(employeeDetailDto);
        }
        catch(NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("")]
    public async Task<ActionResult<EmployeeDetailDto>> Create(EmployeeCreateDto employeeCreateDto)
    {
        try {
            EmployeeDetailDto employeeDetailDto = await _employeesService.Create(employeeCreateDto);

            return CreatedAtAction(nameof(GetAll), new { id = employeeDetailDto.Id }, employeeDetailDto);
        }
        catch(NotFoundException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, EmployeeUpdateDto employeeUpdateDto)
    {
        try
        {
            await _employeesService.Update(id, employeeUpdateDto);

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
            await _employeesService.Delete(id);

            return NoContent();
        }
        catch(NotFoundException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
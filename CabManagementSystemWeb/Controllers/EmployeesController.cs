using Microsoft.AspNetCore.Mvc;
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

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
    public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
    {
        IEnumerable<Employee> employees = await _employeesService.GetAll();

        return new JsonResult(employees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetById(int id)
    {
        try
        {
            Employee? employee = await _employeesService.GetById(id);

            return new JsonResult(employee);
        }
        catch(NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("")]
    public async Task<ActionResult<Employee>> Create(Employee employee)
    {
        try {
            employee = await _employeesService.Create(employee);

            return CreatedAtAction(nameof(GetAll), new { id = employee.Id }, employee);
        }
        catch(NotFoundException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Employee employee)
    {
        try
        {
            await _employeesService.Update(id, employee);

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
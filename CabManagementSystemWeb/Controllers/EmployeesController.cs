using Microsoft.AspNetCore.Mvc;
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Contracts;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace CabManagementSystemWeb.Controllers;

[Route("api/employees")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeesService _employeeService;

    public EmployeesController(IEmployeesService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
    {
        IEnumerable<Employee> employees = await _employeeService.GetAll();
        return new JsonResult(employees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetById(int id)
    {
        Employee? employee = await _employeeService.GetById(id);

        if (employee == null)
        {
            return NotFound();
        }

        return new JsonResult(employee);
    }

    [HttpPost("")]
    public async Task<ActionResult<Employee>> Create(Employee employee)
    {
        employee = await _employeeService.Create(employee);

        return CreatedAtAction(nameof(GetAll), new { id = employee.Id }, employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Employee employee)
    {
        Employee? response = await _employeeService.Update(id, employee);

        if (response == null)
        {
            return BadRequest("The employee with that id does not exist");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Employee? response = await _employeeService.Delete(id);

        if (response == null)
        {
            return BadRequest("The employee with that id does not exist");
        }

        return NoContent();
    }
}
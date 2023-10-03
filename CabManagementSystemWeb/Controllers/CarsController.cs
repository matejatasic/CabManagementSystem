using Microsoft.AspNetCore.Mvc;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Controllers;

[Route("api/cars")]
[ApiController]
public class CarsController : ControllerBase
{
    private readonly ICarsService _carsService;

    public CarsController(ICarsService carsService)
    {
        _carsService = carsService;
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<CarDetailDto>>> GetAll()
    {
        IEnumerable<CarDetailDto> cars = await _carsService.GetAll();

        return new JsonResult(cars);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CarDetailDto>> GetById(int id)
    {
        try
        {
            CarDetailDto? carDetailDto = await _carsService.GetById(id);

            return new JsonResult(carDetailDto);
        }
        catch(NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("")]
    public async Task<ActionResult<CarDetailDto>> Create(CarCreateDto carCreateDto)
    {
        try {
            CarDetailDto carDetailDto = await _carsService.Create(carCreateDto);

            return CreatedAtAction(nameof(GetAll), new { id = carDetailDto.Id }, carDetailDto);
        }
        catch(NotFoundException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CarUpdateDto carUpdateDto)
    {
        try
        {
            await _carsService.Update(id, carUpdateDto);

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
            await _carsService.Delete(id);

            return NoContent();
        }
        catch(NotFoundException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
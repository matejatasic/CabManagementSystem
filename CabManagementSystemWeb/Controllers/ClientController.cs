using Microsoft.AspNetCore.Mvc;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Controllers;

[Route("api/clients")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IClientsService _clientsService;

    public ClientsController(IClientsService clientsService)
    {
        _clientsService = clientsService;
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<ClientDetailDto>>> GetAll()
    {
        IEnumerable<ClientDetailDto> clients = await _clientsService.GetAll();

        return new JsonResult(clients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClientDetailDto>> GetById(int id)
    {
        try
        {
            ClientDetailDto? clientDetailDto = await _clientsService.GetById(id);

            return new JsonResult(clientDetailDto);
        }
        catch(NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("")]
    public async Task<ActionResult<ClientDetailDto>> Create(ClientCreateDto clientCreateDto)
    {
        try {
            ClientDetailDto clientDetailDto = await _clientsService.Create(clientCreateDto);

            return CreatedAtAction(nameof(GetAll), new { id = clientDetailDto.Id }, clientDetailDto);
        }
        catch(NotFoundException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ClientUpdateDto clientUpdateDto)
    {
        try
        {
            await _clientsService.Update(id, clientUpdateDto);

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
            await _clientsService.Delete(id);

            return NoContent();
        }
        catch(NotFoundException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
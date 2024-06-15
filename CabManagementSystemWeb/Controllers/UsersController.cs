using Microsoft.AspNetCore.Mvc;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace CabManagementSystemWeb.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<UserDetailDto>>> GetAll()
    {
        IEnumerable<UserDetailDto> users = await _usersService.GetAll();

        return new JsonResult(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDetailDto>> GetById(int id)
    {
        try
        {
            UserDetailDto? userDetailDto = await _usersService.GetById(id);

            return new JsonResult(userDetailDto);
        }
        catch(NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("")]
    public async Task<ActionResult<UserDetailDto>> Create(UserCreateDto userCreateDto)
    {
        try {
            UserDetailDto userDetailDto = await _usersService.Create(userCreateDto);

            return CreatedAtAction(nameof(GetAll), new { id = userDetailDto.Id }, userDetailDto);
        }
        catch(NotFoundException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UserUpdateDto userUpdateDto)
    {
        try
        {
            await _usersService.Update(id, userUpdateDto);

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
            await _usersService.Delete(id);

            return NoContent();
        }
        catch(NotFoundException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
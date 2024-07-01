using AuthenticationApi.Services;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CabManagementSystemWeb.Controllers;

[Route("api/register")]
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public RegisterController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("")]
    public async Task<ActionResult<AuthenticationResponseDto>> Register(RegisterDto registerDto)
    {
        try {
            return await _authenticationService.Register(registerDto);
        }
        catch (NotFoundException exception) {
            return BadRequest(exception.Message);
        }
        catch(ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
        catch(Exception exception)
        {
            return StatusCode(500, "There was an unknown error, please try again");
        }
    }
}
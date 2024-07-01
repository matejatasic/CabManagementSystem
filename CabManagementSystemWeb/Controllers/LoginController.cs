using AuthenticationApi.Services;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CabManagementSystemWeb.Controllers;

[Route("api/login")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public LoginController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("")]
    public async Task<ActionResult<AuthenticationResponseDto>> Login(LoginDto loginDto)
    {
        try {
            return await _authenticationService.Login(loginDto);
        }
        catch (NotFoundException exception)
        {
            return BadRequest(exception.Message);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
        catch (Exception exception)
        {
            return StatusCode(500, "There was an unknown error, please try again");
        }
    }
}
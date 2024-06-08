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
    public async Task<ActionResult<string>> Login(LoginDto loginDto)
    {
        try {
            string token = await _authenticationService.Login(loginDto);

            return token;
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
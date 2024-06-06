using AuthenticationApi.Services;
using CabManagementSystemWeb.Dtos;
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
    public async Task<ActionResult<string>> Register(RegisterDto registerDto)
    {
        try {
            string token = await _authenticationService.Register(registerDto);

            return token;
        }
        catch(ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
        catch(Exception)
        {
            return StatusCode(500, "There was an unknown error, please try again");
        }
    }
}
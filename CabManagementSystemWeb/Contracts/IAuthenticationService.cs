using CabManagementSystemWeb.Dtos;

namespace AuthenticationApi.Services;

public interface IAuthenticationService
{
    Task<string> Register(RegisterDto request);
    Task<string> Login(LoginDto request);
}
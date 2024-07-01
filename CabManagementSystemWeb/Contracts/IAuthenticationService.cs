using CabManagementSystemWeb.Dtos;

namespace AuthenticationApi.Services;

public interface IAuthenticationService
{
    Task<AuthenticationResponseDto> Register(RegisterDto request);
    Task<AuthenticationResponseDto> Login(LoginDto request);
}
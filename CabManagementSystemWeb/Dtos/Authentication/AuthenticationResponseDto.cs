namespace CabManagementSystemWeb.Dtos;

public class AuthenticationResponseDto
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }
}
namespace CabManagementSystemWeb.Dtos;

public class AuthenticationResponseDto
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }
}
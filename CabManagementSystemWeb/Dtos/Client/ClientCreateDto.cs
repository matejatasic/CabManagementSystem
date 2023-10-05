using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Dtos;

public class ClientCreateDto : IEntityCreateDto<Client>
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public Client ConvertToEntity()
    {
        return new Client()
        {
            Username = Username,
            Password = Password,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName
        };
    }
}
using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Dtos;

public class ClientUpdateDto : IEntityUpdateDto<Client>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public Client ConvertToEntity(int id)
    {
        return new Client()
        {
            Id = id,
            Username = Username,
            Password = Password,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName
        };
    }
}
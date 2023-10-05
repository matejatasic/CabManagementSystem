using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Dtos;

public class ClientDetailDto : IEntityDetailDto<Client>
{
    public required int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
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
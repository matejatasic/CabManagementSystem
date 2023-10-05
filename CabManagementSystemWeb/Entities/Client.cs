using System.ComponentModel.DataAnnotations.Schema;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Entities;

[Table("Clients")]
public class Client : IEntity
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; } = null;

    public ClientDetailDto ConvertToDetailDto()
    {
        return new ClientDetailDto() {
            Id = Id,
            Username = Username,
            Password = Password,
            Email = Email,
            FirstName =  FirstName,
            LastName = LastName
        };
    }
}
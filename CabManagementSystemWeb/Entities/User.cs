using System.ComponentModel.DataAnnotations.Schema;

namespace CabManagementSystemWeb.Entities;

using CabManagementSystemWeb.Data;

[Table("Users")]
public class User: IEntity
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; } = null;
}
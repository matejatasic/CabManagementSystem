using CabManagementSystemWeb.Data;

namespace CabManagementSystemWeb.Entities;

public class Employee : IEntity
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}
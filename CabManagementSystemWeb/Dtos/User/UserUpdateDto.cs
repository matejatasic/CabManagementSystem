using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Dtos;

public class UserUpdateDto : IEntityUpdateDto<User>
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? RoleId { get; set; }
}
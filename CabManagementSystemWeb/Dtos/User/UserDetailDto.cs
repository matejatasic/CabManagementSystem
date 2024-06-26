using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Dtos;

public class UserDetailDto : IEntityDetailDto<User>
{
    public required int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public required int RoleId { get; set; }

    public User ConvertToEntity()
    {
        return new User()
        {
            Id = Id,
            UserName = Username,
            Password = Password,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            RoleId = RoleId
        };
    }
}
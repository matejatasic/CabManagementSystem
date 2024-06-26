using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Dtos;

public class UserUpdateDto : IEntityUpdateDto<User>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int RoleId { get; set; }

    public User ConvertToEntity(int id)
    {
        return new User()
        {
            Id = id,
            UserName = Username,
            Password = Password,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            RoleId = RoleId
        };
    }
}
using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Dtos;

public class UserCreateDto : IEntityCreateDto<User>
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public required int RoleId { get; set; }

    public User ConvertToEntity()
    {
        return new User()
        {
            Username = Username,
            Password = Password,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            Address = Address,
            PhoneNumber = PhoneNumber,
            RoleId = RoleId
        };
    }
}
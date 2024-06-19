using System.ComponentModel.DataAnnotations.Schema;

namespace CabManagementSystemWeb.Entities;

using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using Microsoft.AspNetCore.Identity;

[Table("Users")]
public class User: IdentityUser<int>, IEntity
{
    public required string Password { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public Role? Role { get; set; }
    public required int RoleId { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; } = null;

    public UserDetailDto ConvertToDetailDto()
    {
        return new UserDetailDto()
        {
            Id = Id,
            Username = UserName,
            Password = Password,
            Email = Email,
            FirstName =  FirstName,
            LastName = LastName,
            RoleId = RoleId
        };
    }
}
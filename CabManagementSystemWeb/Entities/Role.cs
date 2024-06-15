namespace CabManagementSystemWeb.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using Microsoft.AspNetCore.Identity;

[Table("Roles")]
public class Role: IdentityRole, IEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; } = null;

    public RoleDetailDto ConvertToDetailDto()
    {
        return new RoleDetailDto()
        {
            Id = Id,
            Name = Name
        };
    }
}
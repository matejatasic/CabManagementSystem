using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Dtos;

public class RoleUpdateDto : IEntityUpdateDto<Role>
{
    public required string Name { get; set; }

    public Role ConvertToEntity(int id)
    {
        return new Role()
        {
            Id = id,
            Name = Name,
        };
    }
}
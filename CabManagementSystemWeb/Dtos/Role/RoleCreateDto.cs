using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Dtos;

public class RoleCreateDto : IEntityCreateDto<Role>
{
    public required string Name { get; set; }

    public Role ConvertToEntity()
    {
        return new Role()
        {
            Name = Name,
        };
    }
}
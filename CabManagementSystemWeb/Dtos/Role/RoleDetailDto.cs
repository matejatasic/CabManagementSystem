using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Dtos;

public class RoleDetailDto : IEntityDetailDto<Role>
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public Role ConvertToEntity()
    {
        return new Role()
        {
            Id = Id,
            Name = Name
        };
    }
}
using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Dtos;

public class BranchCreateDto : IEntityCreateDto<Branch>
{
    public required string Name { get; set; }
    public int? ManagerId { get; set; }

    public Branch ConvertToEntity()
    {
        return new Branch() {
            Name = Name,
            ManagerId = ManagerId
        };
    }
}
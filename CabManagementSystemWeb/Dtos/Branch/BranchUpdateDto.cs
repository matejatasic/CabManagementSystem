using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Dtos;

public class BranchUpdateDto : IEntityUpdateDto<Branch>
{
    public string Name { get; set; } = string.Empty;
    public int? ManagerId { get; set; }

    public Branch ConvertToEntity(int id)
    {
        return new Branch() {
            Id = id,
            Name = Name,
            ManagerId = ManagerId
        };
    }
}
using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Dtos;

public class BranchUpdateDto : IEntityUpdateDto<Branch>
{
    public string? Name { get; set; }
    public int? ManagerId { get; set; }
}
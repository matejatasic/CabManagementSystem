using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Dtos;

public class BranchDetailDto : IEntityDetailDto<Branch>
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public int? ManagerId { get; set; }
    public ICollection<int>? EmployeesIds { get; set; }

    public Branch ConvertToEntity()
    {
        return new Branch() {
            Name = Name,
            ManagerId = ManagerId
        };
    }
}
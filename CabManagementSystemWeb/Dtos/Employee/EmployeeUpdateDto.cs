using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Dtos;

public class EmployeeUpdateDto : IEntityUpdateDto<Employee>
{
    public int? UserId { get; set; }
    public int? BranchId { get; set; }

    public Employee ConvertToEntity(int id)
    {
        return new Employee()
        {
            Id = id,
            UserId = UserId ?? 0,
            BranchId = BranchId ?? 0
        };
    }
}
using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Dtos;

public class EmployeeUpdateDto : IEntityUpdateDto<Employee>
{
    public int? UserId { get; set; }
    public int? BranchId { get; set; }
}
using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Dtos;

public class EmployeeCreateDto : IEntityCreateDto<Employee>
{
    public required int UserId { get; set; }
    public required int BranchId { get; set; }

    public Employee ConvertToEntity()
    {
        return new Employee()
        {
            UserId = UserId,
            BranchId = BranchId
        };
    }
}
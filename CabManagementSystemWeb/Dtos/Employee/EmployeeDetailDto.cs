using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Dtos;

public class EmployeeDetailDto : IEntityDetailDto<Employee>
{
    public required int Id { get; set; }
    public required int UserId { get; set; }
    public required int BranchId { get; set; }

    public Employee ConvertToEntity()
    {
        return new Employee()
        {
            Id = Id,
            UserId = UserId,
            BranchId = BranchId
        };
    }
}
using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Dtos;

public class EmployeeDetailDto : IEntityDetailDto<Employee>
{
    public required int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public required int UserId { get; set; }
    public required int BranchId { get; set; }

    public Employee ConvertToEntity(int id)
    {
        return new Employee()
        {
            Id = id,
            UserId = UserId,
            BranchId = BranchId
        };
    }
}
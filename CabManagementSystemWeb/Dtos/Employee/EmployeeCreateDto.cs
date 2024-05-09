using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Entities;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace CabManagementSystemWeb.Dtos;

public class EmployeeCreateDto : IEntityCreateDto<Employee>
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
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
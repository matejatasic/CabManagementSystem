using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Dtos;

public class EmployeeUpdateDto : IEntityUpdateDto<Employee>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int? BranchId { get; set; }

    public Employee ConvertToEntity(int id)
    {
        return new Employee()
        {
            Id = id,
            Username = Username,
            Password = Password,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            Address = Address,
            BranchId = BranchId ?? 0
        };
    }
}
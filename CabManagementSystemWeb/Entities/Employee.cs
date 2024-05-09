using System.ComponentModel.DataAnnotations.Schema;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Entities;

[Table("Employees")]
public class Employee : IEntity
{
    public int Id { get; set; }
    public User? User { get; set; }
    public required int UserId { get; set; }
    public Branch? Branch { get; set; }
    public required int BranchId { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; } = null;

    public EmployeeDetailDto ConvertToDetailDto()
    {
        return new EmployeeDetailDto() {
            Id = Id,
            Username = User.Username,
            Password = User.Password,
            Email = User.Email,
            FirstName =  User.FirstName,
            LastName = User.LastName,
            Address = User.Address,
            UserId = UserId,
            BranchId = BranchId
        };
    }
}
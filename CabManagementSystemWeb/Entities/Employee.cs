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
            UserId = UserId,
            BranchId = BranchId
        };
    }
}
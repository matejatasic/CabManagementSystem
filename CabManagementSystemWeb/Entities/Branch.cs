using System.ComponentModel.DataAnnotations.Schema;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Entities;

[Table("Branches")]
public class Branch : IEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public Employee? Manager { get; set; }
    public int? ManagerId { get; set; }
    [InverseProperty("Branch")]
    public ICollection<Employee>? Employees { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; } = null;


    public BranchDetailDto ConvertToDetailDto()
    {
        return new BranchDetailDto() {
            Id = Id,
            Name = Name,
            ManagerId = ManagerId,
            EmployeesIds = Employees?.Select(e => e.Id).ToList()
        };
    }
}

using System.ComponentModel.DataAnnotations.Schema;

using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Entities;

[Table("Cars")]
public class Car : IEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int NumberOfSeats { get; set; }
    public required string FuelType { get; set; }
    public DateTime? RegisteredUntil { get; set; }
    public string RegistrationPlates { get; set; } = string.Empty;
    public Employee? Driver { get; set; }
    public required int DriverId { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; } = null;

    public CarDetailDto ConvertToDetailDto()
    {
        return new CarDetailDto() {
            Id = Id,
            Name = Name,
            NumberOfSeats = NumberOfSeats,
            FuelType = FuelType,
            RegisteredUntil = RegisteredUntil,
            RegistrationPlates = RegistrationPlates,
            DriverId = DriverId
        };
    }
}
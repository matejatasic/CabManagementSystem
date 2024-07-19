using CabManagementSystemWeb.Dtos.Interfaces;

namespace CabManagementSystemWeb.Dtos;

public class CarUpdateDto : IEntityUpdateDto<Car>
{
    public string? Name { get; set; }
    public int? NumberOfSeats { get; set; }
    public string? FuelType { get; set; }
    public DateTime? RegisteredUntil { get; set; }
    public string? RegistrationPlates { get; set; }
    public int? DriverId { get; set; }
}
using CabManagementSystemWeb.Dtos.Interfaces;

namespace CabManagementSystemWeb.Dtos;

public class CarDetailDto : IEntityDetailDto<Car>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int NumberOfSeats { get; set; }
    public required string FuelType { get; set; }
    public DateTime? RegisteredUntil { get; set; }
    public string? RegistrationPlates { get; set; } = string.Empty;
    public required int DriverId { get; set; }

    public Car ConvertToEntity()
    {
        return new Car()
        {
            Id = Id,
            NumberOfSeats = NumberOfSeats,
            Name = Name,
            FuelType = FuelType,
            RegisteredUntil = RegisteredUntil,
            RegistrationPlates = RegistrationPlates,
            DriverId = DriverId
        };
    }
}
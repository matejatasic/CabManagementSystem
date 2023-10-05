using CabManagementSystemWeb.Dtos.Interfaces;

namespace CabManagementSystemWeb.Dtos;

public class CarCreateDto : IEntityCreateDto<Car>
{
    public required string Name { get; set; }
    public required string FuelType { get; set; }
    public DateTime? RegisteredUntil { get; set; } = null;
    public string? RegistrationPlates { get; set; } = string.Empty;
    public required int DriverId { get; set; }

    public Car ConvertToEntity()
    {
        return new Car()
        {
            Name = Name,
            FuelType = FuelType,
            RegisteredUntil = RegisteredUntil,
            RegistrationPlates = RegistrationPlates,
            DriverId = DriverId
        };
    }
}
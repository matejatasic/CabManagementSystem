using CabManagementSystemWeb.Dtos.Interfaces;

namespace CabManagementSystemWeb.Dtos;

public class CarDetailDto : IEntityDetailDto<Car>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string FuelType { get; set; }
    public DateTime? RegisteredUntil { get; set; }
    public string? RegistrationPlates { get; set; } = string.Empty;
    public required int DriverId { get; set; }

    public Car ConvertToEntity(int id)
    {
        return new Car() {
            Id = id,
            Name = Name,
            FuelType = FuelType,
            RegisteredUntil = RegisteredUntil,
            RegistrationPlates = RegistrationPlates,
            DriverId = DriverId
        };
    }
}
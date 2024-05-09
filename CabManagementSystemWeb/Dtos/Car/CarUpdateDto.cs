using CabManagementSystemWeb.Dtos.Interfaces;

namespace CabManagementSystemWeb.Dtos;

public class CarUpdateDto : IEntityUpdateDto<Car>
{
    public string Name { get; set; } = string.Empty;
    public int NumberOfSeats { get; set; }
    public string FuelType { get; set; } = string.Empty;
    public DateTime? RegisteredUntil { get; set; }
    public string? RegistrationPlates { get; set; } = string.Empty;
    public int DriverId { get; set; }

    public Car ConvertToEntity(int id)
    {
        return new Car()
        {
            Id = id,
            Name = Name,
            NumberOfSeats = NumberOfSeats,
            FuelType = FuelType,
            RegisteredUntil = RegisteredUntil,
            RegistrationPlates = RegistrationPlates,
            DriverId = DriverId
        };
    }
}
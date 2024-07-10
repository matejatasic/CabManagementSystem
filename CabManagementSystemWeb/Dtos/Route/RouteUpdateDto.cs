using CabManagementSystemWeb.Dtos.Interfaces;

namespace CabManagementSystemWeb.Dtos;

public class RouteUpdateDto : IEntityUpdateDto<Route>
{
    public string FromAddress { get; set; } = string.Empty;
    public string ToAddress { get; set; } = string.Empty;
    public float? TravelCost {get; set; }
    public required int TravelerId { get; set; }
    public required int DriverId { get; set; }

    public Route ConvertToEntity(int id)
    {
        return new Route()
        {
            Id = id,
            FromAddress = FromAddress,
            ToAddress = ToAddress,
            TravelCost = TravelCost,
            TravelerId = TravelerId,
            DriverId = DriverId
        };
    }
}
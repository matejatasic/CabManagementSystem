using CabManagementSystemWeb.Dtos.Interfaces;

namespace CabManagementSystemWeb.Dtos;

public class RouteCreateDto : IEntityCreateDto<Route>
{
    public required string FromAddress { get; set; }
    public required string ToAddress { get; set; }
    public float? TravelCost {get; set; }
    public required int TravelerId { get; set; }
    public required int DriverId { get; set; }

    public Route ConvertToEntity()
    {
        return new Route()
        {
            FromAddress = FromAddress,
            ToAddress = ToAddress,
            TravelCost = TravelCost,
            TravelerId = TravelerId,
            DriverId = DriverId
        };
    }
}
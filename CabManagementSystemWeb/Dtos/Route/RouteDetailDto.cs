using CabManagementSystemWeb.Dtos.Interfaces;

namespace CabManagementSystemWeb.Dtos;

public class RouteDetailDto : IEntityDetailDto<Route>
{
    public int Id { get; set; }
    public required string FromAddress { get; set; }
    public required string ToAddress { get; set; }
    public float? TravelCost {get; set; }
    public required int DriverId { get; set; }

    public Route ConvertToEntity(int id)
    {
        return new Route()
        {
            Id = id,
            FromAddress = FromAddress,
            ToAddress = ToAddress,
            TravelCost = TravelCost,
            DriverId = DriverId
        };
    }
}
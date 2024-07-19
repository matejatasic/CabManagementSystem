using CabManagementSystemWeb.Dtos.Interfaces;

namespace CabManagementSystemWeb.Dtos;

public class RouteUpdateDto : IEntityUpdateDto<Route>
{
    public string? FromAddress { get; set; }
    public string? ToAddress { get; set; }
    public float? TravelCost {get; set; }
    public int? TravelerId { get; set; }
    public int? DriverId { get; set; }
}
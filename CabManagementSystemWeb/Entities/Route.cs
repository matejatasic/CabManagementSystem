using System.ComponentModel.DataAnnotations.Schema;

using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Entities;

[Table("Routes")]
public class Route : IEntity
{
    public int Id { get; set; }
    public required string FromAddress { get; set; }
    public required string ToAddress { get; set; }
    public float? TravelCost {get; set; }
    public Employee? Driver { get; set; }
    public required int DriverId { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; } = null;

    public RouteDetailDto ConvertToDetailDto()
    {
        return new RouteDetailDto() {
            Id = Id,
            FromAddress = FromAddress,
            ToAddress = ToAddress,
            TravelCost = TravelCost,
            DriverId = DriverId
        };
    }
}
namespace BuildingBlocks.Shared.DTO.Ride;

public class RequestRideDto
{
    public Guid PassengerId { get; set; }
    public double FromLatitude { get; set; }
    public double FromLongitude { get; set; }
    public double ToLatitude { get; set; }
    public double ToLongitude { get; set; }
    
}
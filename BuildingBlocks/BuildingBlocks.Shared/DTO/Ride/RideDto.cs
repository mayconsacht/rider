namespace BuildingBlocks.Shared.DTO.Ride;

public record RideDto
{
    public Guid Id { get; set; }
    public double FromLatitude { get; set; }
    public double FromLongitude { get; set; }
    public double ToLatitude { get; set; }
    public double ToLongitude { get; set; }
    public Guid PassengerId { get; set; }
    public string PassengerName { get; set; }
    public Guid DriverId { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }
    public double? CurrentLatitude { get; set; }
    public double? CurrentLongitude { get; set; }
    public double Distance { get; set; }
    public double Fare { get; set; }
}
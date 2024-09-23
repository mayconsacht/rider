using Ride.Domain.Exceptions;

namespace Ride.Domain.ValueObject;

public record Coordinate()
{
    private const double MinLatitude = -90;
    private const double MaxLatitude = 90;
    private const double MinLongitude = -180;
    private const double MaxLongitude = 180;
    
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    
    public Coordinate(double latitude, double longitude) : this()
    {
        if (latitude is < MinLatitude or > MaxLatitude) throw new RideDomainException("Invalid latitude");
        if (longitude is < MinLongitude or > MaxLongitude) throw new RideDomainException("Invalid longitude");
        Latitude = latitude;
        Longitude = longitude;
    }
}
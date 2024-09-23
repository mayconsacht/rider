using Ride.Domain.Exceptions;

namespace Ride.Domain.ValueObject;

public record Segment
{
    private const double EarthRadius = 6371;
    private const double DegreesToRadians = Math.PI / 180;
    
    public Coordinate From { get; init; }
    public Coordinate To { get; init; }

    public Segment(Coordinate from, Coordinate to)
    {
        if (from is null || to is null) throw new RideDomainException("Invalid segment");
        From = from;
        To = to;
    }

    public double GetDistance() {
        var deltaLatitude = (To.Latitude - From.Latitude) * DegreesToRadians;
        var deltaLongitude = (To.Longitude - From.Longitude) * DegreesToRadians;
        var angularDistanceFactor =
        Math.Sin(deltaLatitude / 2) * Math.Sin(deltaLatitude / 2) +
            Math.Cos(From.Latitude * DegreesToRadians) *
            Math.Cos(To.Latitude * DegreesToRadians) *
            Math.Sin(deltaLongitude / 2) *
            Math.Sin(deltaLongitude / 2);
        var centralAngle = 2 * Math.Atan2(Math.Sqrt(angularDistanceFactor), Math.Sqrt(1 - angularDistanceFactor));
        var distance = EarthRadius * centralAngle;
        return Math.Round(distance);
    }
}
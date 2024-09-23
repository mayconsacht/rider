using Shared.Domain;
using Ride.Domain.ValueObject;

namespace Ride.Domain.Entities;

public sealed class Position : Entity
{
    public Guid RideId { get; private set; }
    public Coordinate Coordinate { get; private set; }
    public DateTime Date { get; private set; }

    public Position() { }

    public Position(Guid id, Guid rideId, double latitude, double longitude, DateTime date)
    {
        Id = id;
        RideId = rideId;
        Coordinate = new Coordinate(latitude, longitude);
        Date = date;
    }

    public static Position Create(Guid rideId, double latitude, double longitude, DateTime date)
    {
        return new Position(Guid.NewGuid(), rideId, latitude, longitude, date);
    }
}
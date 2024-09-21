using Ride.Domain.Entities;

namespace Ride.Application.Repository;

public interface IPositionRepository
{
    Task SavePosition(Position position);
    Task<Position?> GetLastPositionFromRideId(Guid id);
}
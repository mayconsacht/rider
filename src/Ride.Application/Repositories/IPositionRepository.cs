using Ride.Domain.Entities;

namespace Ride.Application.Repositories;

public interface IPositionRepository
{
    Task<Guid?> SavePosition(Position position);
    Task<Position?> GetLastPositionFromRideId(Guid id);
}
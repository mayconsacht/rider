using RideEntity = Ride.Domain.Entities.Ride;

namespace Ride.Application.Repository;

public interface IRideRepository
{
    Task<Guid?> Save(RideEntity ride);
    Task<RideEntity> GetRideById(Guid id);
    Task<bool> HasActiveRideByPassengerId(Guid passengerId);
    Task<bool> HasActiveRideByDriverId(Guid driverId);
    Task<RideEntity> UpdateRide(RideEntity ride);
}
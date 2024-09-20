using Ride.Domain.Enums;
using RideEntity = Ride.Domain.Entities.Ride;

namespace Tests.Mocks;

public static class RideMock
{
    public class Entity()
    {
        public static RideEntity CreateAccepted(Guid? rideId = null)
        {
            return new RideEntity(rideId ?? Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), 10, 43, 130, 100, RideStatus.Accepted, 
                DateTimeOffset.UtcNow.Date, 280, 10);
        }
        
        public static RideEntity CreateRequested(Guid? rideId = null)
        {
            return new RideEntity(rideId ?? Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), 10, 43, 130, 100, RideStatus.Requested, 
                DateTimeOffset.UtcNow.Date, 280, 10);
        }
        
        public static RideEntity CreateInProgress(Guid? rideId = null)
        {
            return new RideEntity(rideId ?? Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), 10, 43, 130, 100, RideStatus.InProgress, 
                DateTimeOffset.UtcNow.Date, 280, 10);
        }
    }
}
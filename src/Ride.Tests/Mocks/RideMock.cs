using Shared.DTO.Ride;
using Ride.Domain.Enums;
using RideEntity = Ride.Domain.Entities.Ride;

namespace Ride.Tests.Mocks;

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

    public class DTO()
    {
        public static RequestRideDto CreateRequestRideDto(Guid? passengerId = null)
        {
            return new RequestRideDto()
            {
                PassengerId = passengerId ?? Guid.NewGuid(),
                FromLatitude = 10,
                FromLongitude = 45,
                ToLatitude = 78,
                ToLongitude = 130
            };
        }

        public static AcceptRideDto CreateAcceptRideDto(Guid? rideId = null, Guid? driverId = null)
        {
            return new AcceptRideDto()
            {
                RideId = rideId ?? Guid.NewGuid(),
                DriverId = driverId ?? Guid.NewGuid()
            };
        }
    }
}
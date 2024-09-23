using Ride.Domain.Entities;
using Ride.Domain.Enums;

namespace Ride.Tests.Mocks;

public static class PositionMock
{
    public class Entity()
    {
        public static Position Create(Guid? positionId = null, Guid? rideId = null)
        {
            return new Position(positionId ?? Guid.NewGuid(), rideId ?? Guid.NewGuid(),
                10, 45, DateTime.Now);
        }
    }
}
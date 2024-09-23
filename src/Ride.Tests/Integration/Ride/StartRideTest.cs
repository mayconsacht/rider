using Ride.Application.UseCases.Ride;
using Ride.Domain.Enums;
using Ride.Tests.Mocks;

namespace Ride.Tests.Integration.Ride;

public class StartRideTest : TestBase
{
    [Test]
    public async Task AcceptRide_ShouldUpdateRideStatusToInProgress()
    {
        // Arrange
        var ride = RideMock.Entity.CreateAccepted();
        var startRide = new StartRide(RideRepository);

        Context.Rides.Add(ride);
        await Context.SaveChangesAsync();
        
        // Act
        var result = await startRide.Execute(ride.Id);
        
        // Assert
        Assert.That(result, Is.Not.Null);

        var rideFromDb = await RideRepository.GetRideById(ride.Id);
        Assert.That(rideFromDb.Status, Is.EqualTo(RideStatus.InProgress));
    }

}

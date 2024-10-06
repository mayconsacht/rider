using Ride.Application.UseCases.Ride;
using Ride.Application.UseCases.Ride.Commands;
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
        var startRide = new StartRideCommandHandler(RideRepository);
        Context.Rides.Add(ride);
        await Context.SaveChangesAsync();
        var command = new StartRideCommand(ride.Id);

        // Act
        var result = await startRide.Handle(command, default);
        
        // Assert
        Assert.That(result, Is.Not.Null);

        var rideFromDb = await RideRepository.GetRideById(ride.Id);
        Assert.That(rideFromDb.Status, Is.EqualTo(RideStatus.InProgress));
    }

}

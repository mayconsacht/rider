using Ride.Application.UseCases.Ride;
using Ride.Application.UseCases.Ride.Commands;
using Ride.Domain.Enums;
using Ride.Tests.Mocks;

namespace Ride.Tests.Integration.Ride;

public class FinishRideTest : TestBase
{
    [Test]
    public async Task AcceptRide_ShouldUpdateRideStatusToCompleted()
    {
        // Arrange
        var ride = RideMock.Entity.CreateInProgress();
        var finishRide = new FinishRideCommandHandler(RideRepository, RideIntegrationEventService);
        Context.Rides.Add(ride);
        await Context.SaveChangesAsync();
        var command = new FinishRideCommand(ride.Id);

        // Act
        var result = await finishRide.Handle(command, default);
        
        // Assert
        var rideFromDb = await RideRepository.GetRideById(ride.Id);
        Assert.That(rideFromDb.Status, Is.EqualTo(RideStatus.Completed));
    }

}
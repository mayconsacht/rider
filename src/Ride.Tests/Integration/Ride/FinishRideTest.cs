using Ride.Application.UseCases.Ride;
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
        var finishRide = new FinishRide(RideRepository, RideIntegrationEventService);

        Context.Rides.Add(ride);
        await Context.SaveChangesAsync();
        
        // Act
        var result = await finishRide.Execute(ride.Id);
        
        // Assert
        Assert.That(result, Is.Not.Null);

        var rideFromDb = await RideRepository.GetRideById(ride.Id);
        Assert.That(rideFromDb.Status, Is.EqualTo(RideStatus.Completed));
    }

}
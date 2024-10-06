using Moq;
using Ride.Application.Gateways;
using Ride.Application.UseCases.Ride;
using Ride.Tests.Mocks;

namespace Ride.Tests.Integration.Ride;

public class GetRideTest : TestBase
{
    [Test]
    public async Task GetRide_ReturnsRide()
    {
        // Arrange
        var ride = RideMock.Entity.CreateAccepted();
        var position = PositionMock.Entity.Create(Guid.NewGuid(), ride.Id);
        var account = AccountMock.DTO.Create(); 
        var accountGateway = new Mock<IAccountGateway>();
        accountGateway.Setup(acc => acc.GetAccountById(It.IsAny<Guid>()))
            .ReturnsAsync(account);
        var query = new RideQueries(RideRepository, PositionRepository, accountGateway.Object);

        Context.Rides.Add(ride);
        Context.Positions.Add(position);
        await Context.SaveChangesAsync();
        
        // Act
        var rideFromDb = await query.GetRide(ride.Id);
        
        // Assert
        Assert.That(rideFromDb.Id, Is.EqualTo(ride.Id));
        Assert.That(rideFromDb.PassengerId, Is.EqualTo(ride.PassengerId));
        Assert.That(rideFromDb.DriverId, Is.EqualTo(ride.DriverId));
        Assert.That(rideFromDb.PassengerName, Is.EqualTo(account.Name));
        Assert.That(rideFromDb.FromLatitude, Is.EqualTo(ride.From.Latitude));
        Assert.That(rideFromDb.FromLongitude, Is.EqualTo(ride.From.Longitude));
        Assert.That(rideFromDb.ToLatitude, Is.EqualTo(ride.To.Latitude));
        Assert.That(rideFromDb.ToLongitude, Is.EqualTo(ride.To.Longitude));
        Assert.That(rideFromDb.Status, Is.EqualTo(ride.Status.ToString()));
        Assert.That(rideFromDb.Date, Is.EqualTo(ride.Date));
        Assert.That(rideFromDb.CurrentLatitude, Is.EqualTo(position.Coordinate.Latitude));
        Assert.That(rideFromDb.CurrentLongitude, Is.EqualTo(position.Coordinate.Longitude));
        Assert.That(rideFromDb.Distance, Is.EqualTo(ride.Distance));
        Assert.That(rideFromDb.Fare, Is.EqualTo(ride.Fare));
    }
}
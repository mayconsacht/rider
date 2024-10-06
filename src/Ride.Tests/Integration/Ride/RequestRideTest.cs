using Microsoft.Extensions.Logging;
using Moq;
using Ride.Application.Gateways;
using Ride.Application.UseCases.Ride.Commands;
using Ride.Domain.Enums;
using Ride.Tests.Mocks;

namespace Ride.Tests.Integration.Ride;

public class RequestRideTest : TestBase
{
    private Mock<ILogger<RequestRideCommandHandler>> _logger;
    
    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<RequestRideCommandHandler>>();
    }
    
    [Test]
    public async Task RequestRide_ShouldCreateRideRequest()
    {
        // Arrange
        var account = AccountMock.DTO.Create(); 
        var accountGateway = new Mock<IAccountGateway>();
        accountGateway.Setup(acc => acc.GetAccountById(It.IsAny<Guid>()))
            .ReturnsAsync(account);
        var requestRide = new RequestRideCommandHandler(accountGateway.Object, RideRepository, _logger.Object, RideIntegrationEventService);
        var requestRideCommand = RideMock.Command.CreateRequestRideCommand();
        
        // Act
        var rideId = await requestRide.Handle(requestRideCommand, default);
        
        // Assert
        var rideFromDb = await RideRepository.GetRideById(rideId ?? Guid.Empty);
        Assert.That(rideFromDb.Id, Is.EqualTo(rideId));
        Assert.That(rideFromDb.PassengerId, Is.EqualTo(requestRideCommand.PassengerId));
        Assert.That(rideFromDb.Status, Is.EqualTo(RideStatus.Requested));
        Assert.That(rideFromDb.From.Longitude, Is.EqualTo(requestRideCommand.FromLongitude));
        Assert.That(rideFromDb.From.Latitude, Is.EqualTo(requestRideCommand.FromLatitude));
        Assert.That(rideFromDb.To.Latitude, Is.EqualTo(requestRideCommand.ToLatitude));
        Assert.That(rideFromDb.To.Longitude, Is.EqualTo(requestRideCommand.ToLongitude));
    }
    
    [Test]
    public async Task RequestRide_WhenPassengerAlreadyInARide_ShouldThrowException()
    {
        // Arrange
        var rideId = Guid.NewGuid();
        var account = AccountMock.DTO.Create(); 
        var accountGateway = new Mock<IAccountGateway>();
        var ride = RideMock.Entity.CreateRequested(rideId);
        accountGateway.Setup(acc => acc.GetAccountById(It.IsAny<Guid>()))
            .ReturnsAsync(account);
        await RideRepository.Save(ride);
        var requestRideCommand = RideMock.Command.CreateRequestRideCommand(ride.PassengerId);
        var requestRide = new RequestRideCommandHandler(accountGateway.Object, RideRepository, _logger.Object, RideIntegrationEventService);
        
        // Act
        var result = await requestRide.Handle(requestRideCommand, default);
        
        // Assert
        Assert.That(result, Is.Null);
        _logger.Verify(
            x => x.Log(
                It.Is<LogLevel>(l => l == LogLevel.Warning),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == "This passenger has an active ride"),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)), 
            Times.Once); 
    }
    
    [Test]
    public async Task RequestRide_WhenAccountIsntPassenger_ShouldThrowException()
    {
        // Arrange
        var account = AccountMock.DTO.Create(); 
        var accountGateway = new Mock<IAccountGateway>();
        account.IsPassenger = false;
        accountGateway.Setup(acc => acc.GetAccountById(It.IsAny<Guid>()))
            .ReturnsAsync(account);
        var requestRideCommand = RideMock.Command.CreateRequestRideCommand();
        var requestRide = new RequestRideCommandHandler(accountGateway.Object, RideRepository, _logger.Object, RideIntegrationEventService);
        
        // Act
        var result = await requestRide.Handle(requestRideCommand, default);
        
        // Assert
        Assert.IsNull(result);
        _logger.Verify(
            x => x.Log(
                It.Is<LogLevel>(l => l == LogLevel.Warning),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == "This account is not from a passenger"),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)), 
            Times.Once); 
    }
}
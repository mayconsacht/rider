using Microsoft.Extensions.Logging;
using Moq;
using Ride.Application.Gateways;
using Ride.Application.UseCases.Ride;
using Ride.Domain.Enums;
using Ride.Tests.Mocks;

namespace Ride.Tests.Integration.Ride;

public class RequestRideTest : TestBase
{
    private Mock<ILogger<RequestRide>> _logger;
    
    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<RequestRide>>();
    }
    
    [Test]
    public async Task RequestRide_ShouldCreateRideRequest()
    {
        // Arrange
        var account = AccountMock.DTO.Create(); 
        var accountGateway = new Mock<IAccountGateway>();
        accountGateway.Setup(acc => acc.GetAccountById(It.IsAny<Guid>()))
            .ReturnsAsync(account);
        var requestRide = new RequestRide(accountGateway.Object, RideRepository, _logger.Object, RideIntegrationEventService);
        var requestRideDto = RideMock.DTO.CreateRequestRideDto();
        
        // Act
        var rideId = await requestRide.Execute(requestRideDto);
        
        // Assert
        var rideFromDb = await RideRepository.GetRideById(rideId ?? Guid.Empty);
        Assert.That(rideFromDb.Id, Is.EqualTo(rideId));
        Assert.That(rideFromDb.PassengerId, Is.EqualTo(requestRideDto.PassengerId));
        Assert.That(rideFromDb.Status, Is.EqualTo(RideStatus.Requested));
        Assert.That(rideFromDb.From.Longitude, Is.EqualTo(requestRideDto.FromLongitude));
        Assert.That(rideFromDb.From.Latitude, Is.EqualTo(requestRideDto.FromLatitude));
        Assert.That(rideFromDb.To.Latitude, Is.EqualTo(requestRideDto.ToLatitude));
        Assert.That(rideFromDb.To.Longitude, Is.EqualTo(requestRideDto.ToLongitude));
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
        var requestRideDto = RideMock.DTO.CreateRequestRideDto(ride.PassengerId);
        var requestRide = new RequestRide(accountGateway.Object, RideRepository, _logger.Object, RideIntegrationEventService);
        
        // Act
        var result = await requestRide.Execute(requestRideDto);
        
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
        var requestRideDto = RideMock.DTO.CreateRequestRideDto();
        var requestRide = new RequestRide(accountGateway.Object, RideRepository, _logger.Object, RideIntegrationEventService);
        
        // Act
        var result = await requestRide.Execute(requestRideDto);
        
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
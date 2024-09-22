using Microsoft.Extensions.Logging;
using Moq;
using Ride.Application.Gateways;
using Ride.Application.UseCases.Ride;
using Ride.Domain.Enums;
using Ride.Tests.Mocks;

namespace Ride.Tests.Integration.Ride;

public class AcceptRideTest : TestBase
{
    private Mock<ILogger<AcceptRide>> _logger;
    
    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<AcceptRide>>();
    }
    
    [Test]
    public async Task AcceptRide_ShouldUpdateRideStatusToInProgress()
    {
        // Arrange
        var ride = RideMock.Entity.CreateRequested();
        var account = AccountMock.DTO.Create(); 
        var accountGateway = new Mock<IAccountGateway>();
        accountGateway.Setup(acc => acc.GetAccountById(It.IsAny<Guid>()))
            .ReturnsAsync(account);
        var acceptRide = new AcceptRide(RideRepository, accountGateway.Object, _logger.Object);
        var requestRideDto = RideMock.DTO.CreateAcceptRideDto(ride.Id);

        Context.Rides.Add(ride);
        await Context.SaveChangesAsync();
        
        // Act
        var result = await acceptRide.Execute(requestRideDto);
        
        // Assert
        Assert.That(result, Is.Not.Null);

        var rideFromDb = await RideRepository.GetRideById(ride.Id);
        Assert.That(rideFromDb.Status, Is.EqualTo(RideStatus.Accepted));
    }
    
    [Test]
    public async Task RequestRide_WhenDriverAlreadyInAnotherRide_ShouldThrowException()
    {
        // Arrange
        var ride = RideMock.Entity.CreateRequested();
        var account = AccountMock.DTO.Create(); 
        var accountGateway = new Mock<IAccountGateway>();
        accountGateway.Setup(acc => acc.GetAccountById(It.IsAny<Guid>()))
            .ReturnsAsync(account);
        var acceptRide = new AcceptRide(RideRepository, accountGateway.Object, _logger.Object);
        var requestRideDto = RideMock.DTO.CreateAcceptRideDto(ride.Id, ride.DriverId);

        Context.Rides.Add(ride);
        await Context.SaveChangesAsync();
        
        // Act
        var result = await acceptRide.Execute(requestRideDto);
        
        // Assert
        Assert.IsNull(result);
        _logger.Verify(
            x => x.Log(
                It.Is<LogLevel>(l => l == LogLevel.Warning),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == "This driver has an active ride"),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)), 
            Times.Once); 
    }
}
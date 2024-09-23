using Ride.Domain.Entities;
using Ride.Domain.Enums;
using Ride.Domain.Exceptions;
using Ride.Tests.Mocks;
using RideEntity = Ride.Domain.Entities.Ride;

namespace Ride.Tests.Unit.Domain.Entities;

public class RideTest
{
    [Test]
    public void Ride_ShouldBeCreated()
    {
        // Arrange
        var rideMock = RideMock.Entity.CreateAccepted();
        
        // Act
        var rideCreated = RideEntity.Create(rideMock.PassengerId, rideMock.DriverId, rideMock.From.Latitude,
            rideMock.To.Latitude, rideMock.From.Longitude, rideMock.To.Longitude);
        
        // Assert
        Assert.That(rideCreated.PassengerId, Is.EqualTo(rideMock.PassengerId));
        Assert.That(rideCreated.DriverId, Is.EqualTo(rideMock.DriverId));
        Assert.That(rideCreated.From.Latitude, Is.EqualTo(rideMock.From.Latitude));
        Assert.That(rideCreated.From.Longitude, Is.EqualTo(rideMock.From.Longitude));
        Assert.That(rideCreated.To.Latitude, Is.EqualTo(rideMock.To.Latitude));
        Assert.That(rideCreated.To.Longitude, Is.EqualTo(rideMock.To.Longitude));
        Assert.That(rideCreated.Status, Is.EqualTo(RideStatus.Requested));
        Assert.That(rideCreated.Date, Is.EqualTo(rideCreated.Date));
        Assert.That(rideCreated.Distance, Is.EqualTo(rideCreated.Distance));
        Assert.That(rideCreated.Fare, Is.EqualTo(rideCreated.Fare));
    }
    
    [Test]
    public void Ride_WhenFromLongLessThanMinus180_ShouldThrowException()
    {
        // Arrange
        var rideMock = RideMock.Entity.CreateAccepted();
        
        // Act
        var ex = Assert.Throws<RideDomainException>(CreateAccepted);
        
        // Assert
        Assert.That(ex.Message, Is.EqualTo("Invalid longitude"));
        return;
        
        void CreateAccepted() => RideEntity.Create(rideMock.PassengerId, rideMock.DriverId, rideMock.From.Latitude,
            rideMock.To.Latitude, -200, rideMock.To.Longitude);
    }
    
    [Test]
    public void Ride_WhenFromLongMoreThan180_ShouldThrowException()
    {
        // Arrange
        var rideMock = RideMock.Entity.CreateAccepted();
        
        // Act
        var ex = Assert.Throws<RideDomainException>(CreateAccepted);
        
        // Assert
        Assert.That(ex.Message, Is.EqualTo("Invalid longitude"));
        return;
        
        void CreateAccepted() => RideEntity.Create(rideMock.PassengerId, rideMock.DriverId, rideMock.From.Latitude,
            rideMock.To.Latitude, 200, rideMock.To.Longitude);
    }
    
    [Test]
    public void Ride_WhenFromLatMoreThan90_ShouldThrowException()
    {
        // Arrange
        var rideMock = RideMock.Entity.CreateAccepted();
        
        // Act
        var ex = Assert.Throws<RideDomainException>(CreateAccount);
        
        // Assert
        Assert.That(ex.Message, Is.EqualTo("Invalid latitude"));
        return;
        
        void CreateAccount() => RideEntity.Create(rideMock.PassengerId, rideMock.DriverId, 100,
            rideMock.To.Latitude, rideMock.From.Longitude, rideMock.To.Longitude);
    }
    
    [Test]
    public void Ride_WhenFromLatLessThanMinus90_ShouldThrowException()
    {
        // Arrange
        var rideMock = RideMock.Entity.CreateAccepted();
        
        // Act
        var ex = Assert.Throws<RideDomainException>(CreateAccount);
        
        // Assert
        Assert.That(ex.Message, Is.EqualTo("Invalid latitude"));
        return;
        
        void CreateAccount() => RideEntity.Create(rideMock.PassengerId, rideMock.DriverId, -95,
            rideMock.To.Latitude, rideMock.From.Longitude, rideMock.To.Longitude);
    }
    
    [Test]
    public void RideAccept_WhenAccountIsntDriver_ShouldThrowException()
    {
        // Arrange
        var ride = RideMock.Entity.CreateAccepted();
        var accountDto = AccountMock.DTO.Create(param =>
        {
            param.IsDriver = false;
        });
        
        // Act
        var ex = Assert.Throws<RideDomainException>(AcceptRide);
        
        // Assert
        Assert.That(ex.Message, Is.EqualTo("Account is not from a Driver"));
        return;

        void AcceptRide() => ride.Accept(accountDto);
    }
    
    [Test]
    public void RideAccept_WhenStatusIsntRequested_ShouldThrowException()
    {
        // Arrange
        var ride = RideMock.Entity.CreateAccepted();
        var accountDto = AccountMock.DTO.Create();
        
        // Act
        var ex = Assert.Throws<RideDomainException>(AcceptRide);
        
        // Assert
        Assert.That(ex.Message, Is.EqualTo("Invalid status"));
        return;

        void AcceptRide() => ride.Accept(accountDto);
    }
    
    [Test]
    public void RideAccept_ShouldChangeStatusToAccepted()
    {
        // Arrange
        var ride = RideMock.Entity.CreateRequested();
        var accountDto = AccountMock.DTO.Create();
        
        // Act
        ride.Accept(accountDto);
        
        // Assert
        Assert.That(ride.Status, Is.EqualTo(RideStatus.Accepted));
    }
    
    [Test]
    public void RideStart_WhenStatusIsntAccepted_ShouldThrowException()
    {
        // Arrange
        var ride = RideMock.Entity.CreateInProgress();
        
        // Act
        var ex = Assert.Throws<RideDomainException>(StartRide);
        
        // Assert
        Assert.That(ex.Message, Is.EqualTo("Invalid status"));
        return;

        void StartRide() => ride.Start();
    }
    
    [Test]
    public void RideStart_ShouldChangeStatusToInProgress()
    {
        // Arrange
        var ride = RideMock.Entity.CreateAccepted();
        
        // Act
        ride.Start();
        
        // Assert
        Assert.That(ride.Status, Is.EqualTo(RideStatus.InProgress));
    }
    
    [Test]
    public void UpdatePosition_WhenStatusIsntInProgress_ShouldThrowException()
    {
        // Arrange
        var ride = RideMock.Entity.CreateRequested();
        var lastPosition = new Position(Guid.NewGuid(), ride.Id, 70, 70, DateTime.Now);
        var currentPosition = new Position(Guid.NewGuid(), ride.Id, 70, 70, DateTime.Now);
        
        // Act
        var ex = Assert.Throws<RideDomainException>(UpdatePosition);
        
        // Assert
        Assert.That(ex.Message, Is.EqualTo("Invalid status"));
        return;

        void UpdatePosition() => ride.UpdatePosition(lastPosition, currentPosition);
    }
    
    [Test]
    public void UpdatePosition_ShouldChangeDistanceAndCalculateFare()
    {
        // Arrange
        var ride = RideMock.Entity.CreateInProgress();
        var lastPosition = new Position(Guid.NewGuid(), ride.Id, 70, 70, DateTime.Now);
        var currentPosition = new Position(Guid.NewGuid(), ride.Id, 70, 70, DateTime.Now);
        
        // Act
        ride.UpdatePosition(lastPosition, currentPosition);
        
        // Assert
        Assert.That(ride.Distance, Is.EqualTo(280.0d));
        Assert.That(ride.Fare, Is.EqualTo(598.0d));
    }
    
    [Test]
    public void Finish_WhenStatusIsntInProgress_ShouldThrowException()
    {
        // Arrange
        var ride = RideMock.Entity.CreateAccepted();
        
        // Act
        var ex = Assert.Throws<RideDomainException>(FinishRide);
        
        // Assert
        Assert.That(ex.Message, Is.EqualTo("Invalid status"));
        return;

        void FinishRide() => ride.Finish();
    }
    
    [Test]
    public void RideFinish_ShouldChangeStatusToCompleted()
    {
        // Arrange
        var ride = RideMock.Entity.CreateInProgress();
        
        // Act
        ride.Finish();
        
        // Assert
        Assert.That(ride.Status, Is.EqualTo(RideStatus.Completed));
    }
}
using System.Runtime.InteropServices.JavaScript;
using BuildingBlocks.Shared.Domain;
using BuildingBlocks.Shared.DTO.Account;
using Ride.Domain.Exceptions;
using Ride.Domain.Service.FareCalculator;
using Ride.Domain.ValueObject;

namespace Ride.Domain.Entities;

public sealed class Ride : Entity, IAggregateRoot
{
    public Coordinate From { get; private set; }
    public Coordinate To { get; private set; }
    public Guid PassengerId { get; private set; }
    public Guid DriverId { get; private set; }
    //TODO: create enum
    public string Status { get; private set; }
    public DateTime Date { get; private set; }
    public double Distance { get; private set; }
    public double Fare { get; private set; }

    public Ride(Guid id, Guid passengerId, Guid driverId, double fromLatitude, double toLatitude, double fromLongitude,
	    double toLongitude, string status, DateTime date, double distance, double fare)
    {
	    Id = id;
	    PassengerId = passengerId;
	    DriverId = driverId;
	    From = new Coordinate(fromLatitude, fromLongitude);
	    To = new Coordinate(toLatitude, toLongitude);
	    Status = status;
	    Date = date;
	    Distance = distance;
	    Fare = fare;
    }

    public static Ride Create(Guid passengerId, Guid driverId, double fromLatitude, double toLatitude,
	    double fromLongitude, double toLongitude, string status, DateTime date, double distance, double fare)
    {
	    return new Ride(Guid.NewGuid(), passengerId, driverId, fromLatitude, toLatitude, fromLongitude, toLongitude,
		    status, date, distance, fare);
    }

    public void Accept(AccountDto account)
    {
		if (!account.IsDriver) throw new RideDomainException("Account is not from a Driver");
		if (!Status.Equals("Requested")) throw new RideDomainException("Invalid status");
		DriverId = account.Id;
		Status = "Accepted";
    }

    public void Start()
    {
	    if (!Status.Equals("Accepted")) throw new RideDomainException("Invalid status");
	    Status = "InProgress";
    }

    public void UpdatePosition(Position lastPosition, Position currentPosition)
    {
	    if (!Status.Equals("InProgress")) throw new RideDomainException("Invalid status");
	    var segment = new Segment(lastPosition.Coordinate, currentPosition.Coordinate);
	    var distance = segment.GetDistance();
	    Distance += distance;
	    Fare += FareCalculatorFactory.Create(currentPosition.Date).Calculate(Distance);
    }

	public void Finish() {
		if (!Status.Equals("InProgress")) throw new RideDomainException("Invalid status");
		Status = "Completed";
	}
}
using Shared.Application;
using Shared.DTO.Ride;
using Ride.Application.Gateways;
using Ride.Application.Repositories;
using Ride.Application.UseCases.Ride.Queries;
using Ride.Application.UseCases.Ride.Queries.ViewModel;

namespace Ride.Application.UseCases.Ride;

public class RideQueries(
    IRideRepository rideRepository,
    IPositionRepository positionRepository,
    IAccountGateway accountGateway)
    : IRideQueries
{
    public async Task<RideView> GetRide(Guid rideId)
    {
        var ride = await rideRepository.GetRideById(rideId);
        var account = await accountGateway.GetAccountById(ride.PassengerId);
        var lasPosition = await positionRepository.GetLastPositionFromRideId(ride.Id);
        return new RideView
        {
            Id = ride.Id,
            PassengerId = ride.PassengerId,
            DriverId = ride.DriverId,
            PassengerName = account.Name,
            FromLatitude = ride.From.Latitude,
            FromLongitude = ride.From.Longitude,
            ToLatitude = ride.To.Latitude,
            ToLongitude = ride.To.Longitude,
            Status = ride.Status.ToString(),
            Date = ride.Date,
            CurrentLatitude = lasPosition?.Coordinate.Latitude,
            CurrentLongitude = lasPosition?.Coordinate.Longitude,
            Distance = ride.Distance,
            Fare = ride.Fare
        };
    }
}
using BuildingBlocks.Shared.Application;
using BuildingBlocks.Shared.DTO.Ride;
using Ride.Application.Gateways;
using Ride.Application.Repositories;

namespace Ride.Application.UseCases.Ride;

public class GetRide(
    IRideRepository rideRepository,
    IPositionRepository positionRepository,
    IAccountGateway accountGateway)
    : IUseCase<Guid, Task<RideDto>>
{
    public async Task<RideDto> Execute(Guid request)
    {
        var ride = await rideRepository.GetRideById(request);
        var account = await accountGateway.GetAccountById(ride.PassengerId);
        var lasPosition = await positionRepository.GetLastPositionFromRideId(ride.Id);
        return new RideDto
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
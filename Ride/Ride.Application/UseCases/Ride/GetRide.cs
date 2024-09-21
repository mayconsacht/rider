using BuildingBlocks.Shared.Application;
using BuildingBlocks.Shared.DTO.Ride;
using Ride.Application.Gateway;
using Ride.Application.Repository;

namespace Ride.Application.UseCases.Ride;

public class GetRide : IUseCase<Guid, Task<RideDto>>
{
    private readonly IRideRepository _rideRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly IAccountGateway _accountGateway;
    
    public GetRide(IRideRepository rideRepository, IPositionRepository positionRepository, IAccountGateway accountGateway)
    {
        _rideRepository = rideRepository;
        _positionRepository = positionRepository;
        _accountGateway = accountGateway;
    }
    
    public async Task<RideDto> Execute(Guid id)
    {
        var ride = await _rideRepository.GetRideById(id);
        var account = await _accountGateway.GetAccountById(ride.PassengerId);
        var lasPosition = await _positionRepository.GetLastPositionFromRideId(ride.Id);
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
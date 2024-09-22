using BuildingBlocks.Shared.Application;
using BuildingBlocks.Shared.DTO.Ride;
using Microsoft.Extensions.Logging;
using Ride.Application.Gateways;
using Ride.Application.Repositories;

namespace Ride.Application.UseCases.Ride;

public class RequestRide(IAccountGateway accountGateway, IRideRepository rideRepository, ILogger<RequestRide> logger) : IUseCase<RequestRideDto, Task<Guid?>>
{
    public async Task<Guid?> Execute(RequestRideDto request)
    {
        var account = await accountGateway.GetAccountById(request.PassengerId);
        if (account is not null && !account.IsPassenger)
        {
            logger.LogWarning("This account is not from a passenger");
            return null;
        }
        var hasActiveRide = await rideRepository.HasActiveRideByPassengerId(request.PassengerId);
        if (hasActiveRide)
        {
            logger.LogWarning("This passenger has an active ride");
            return null;
        }
        var ride = Domain.Entities.Ride.Create(request.PassengerId, Guid.Empty, request.FromLatitude, request.ToLatitude, request.FromLongitude, request.ToLongitude);
        await rideRepository.Save(ride);
        return ride.Id;
    }
}
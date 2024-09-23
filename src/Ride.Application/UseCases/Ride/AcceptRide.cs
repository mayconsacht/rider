using Shared.Application;
using Shared.DTO.Ride;
using Microsoft.Extensions.Logging;
using Ride.Application.Gateways;
using Ride.Application.Repositories;

namespace Ride.Application.UseCases.Ride;

public class AcceptRide(IRideRepository rideRepository, IAccountGateway accountGateway, ILogger<AcceptRide> logger) : IUseCase<AcceptRideDto, Task<Guid?>>
{
    public async Task<Guid?> Execute(AcceptRideDto request)
    {
        var hasActiveDriver = await rideRepository.HasActiveRideByDriverId(request.DriverId);
        if (hasActiveDriver)
        {
            logger.LogWarning("This driver has an active ride");
            return null;
        }
        var account = await accountGateway.GetAccountById(request.DriverId);
        var ride = await rideRepository.GetRideById(request.RideId);
        ride.Accept(account);
        await rideRepository.UpdateRide(ride);
        return ride.Id;
    }
}
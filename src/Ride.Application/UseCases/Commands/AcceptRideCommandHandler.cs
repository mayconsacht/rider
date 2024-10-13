using MediatR;
using Ride.Application.Gateways;
using Ride.Application.Repositories;
using Microsoft.Extensions.Logging;
using Ride.Application.IntegrationEvents;
using Ride.Application.IntegrationEvents.Events;

namespace Ride.Application.UseCases.Ride.Commands;

public class AcceptRideCommandHandler(IRideRepository rideRepository, IAccountGateway accountGateway, 
    ILogger<AcceptRideCommandHandler> logger, IRideIntegrationEventService rideIntegrationEventService) 
    : IRequestHandler<AcceptRideCommand, Guid?>
{
    public async Task<Guid?> Handle(AcceptRideCommand request, CancellationToken cancellationToken)
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
        var acceptedEvent = new RideAcceptedIntegrtionEvent(ride.Id, account.Name);
        await rideIntegrationEventService.PublishThroughEventBusAsync(acceptedEvent);
        return ride.Id;
    }
}

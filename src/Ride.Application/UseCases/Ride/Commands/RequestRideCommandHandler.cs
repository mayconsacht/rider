using MediatR;
using Microsoft.Extensions.Logging;
using Ride.Application.Gateways;
using Ride.Application.IntegrationEvents;
using Ride.Application.IntegrationEvents.Events;
using Ride.Application.Repositories;

namespace Ride.Application.UseCases.Ride.Commands;

public class RequestRideCommandHandler(IAccountGateway accountGateway, IRideRepository rideRepository, ILogger<RequestRideCommandHandler> logger, IRideIntegrationEventService rideIntegrationEventService) : IRequestHandler<RequestRideCommand, Guid?>
{
    public async Task<Guid?> Handle(RequestRideCommand request, CancellationToken cancellationToken)
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
        var requestedEvent = new RideRequestedIntegrationEvent(ride.Id, ride.PassengerId);
        await rideIntegrationEventService.PublishThroughEventBusAsync(requestedEvent);
        return ride.Id;
    }
}
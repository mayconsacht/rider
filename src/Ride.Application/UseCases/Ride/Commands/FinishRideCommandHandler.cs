using MediatR;
using Ride.Application.IntegrationEvents;
using Ride.Application.IntegrationEvents.Events;
using Ride.Application.Repositories;

namespace Ride.Application.UseCases.Ride.Commands;

public class FinishRideCommandHandler(IRideRepository rideRepository, IRideIntegrationEventService rideIntegrationEventService) : IRequestHandler<FinishRideCommand, Guid>
{
    public async Task<Guid> Handle(FinishRideCommand request, CancellationToken cancellationToken)
    {
        var ride = await rideRepository.GetRideById(request.RideId);
        ride.Finish();
        await rideRepository.UpdateRide(ride);
        var completedEvent = new RideCompletedIntegrationEvent(ride.Id, ride.Fare);
        await rideIntegrationEventService.PublishThroughEventBusAsync(completedEvent);
        return ride.Id;
    }
}

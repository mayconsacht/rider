using Ride.Application.IntegrationEvents;
using Ride.Application.IntegrationEvents.Events;
using Shared.Application;
using Ride.Application.Repositories;

namespace Ride.Application.UseCases.Ride;

public class FinishRide(IRideRepository rideRepository, IRideIntegrationEventService rideIntegrationEventService) : IUseCase<Guid, Task<Guid?>>
{
    public async Task<Guid?> Execute(Guid rideId)
    {
        var ride = await rideRepository.GetRideById(rideId);
        ride.Finish();
        await rideRepository.UpdateRide(ride);
        var completedEvent = new RideCompletedIntegrationEvent(ride.Id, ride.Fare);
        await rideIntegrationEventService.PublishThroughEventBusAsync(completedEvent);
        return ride.Id;
    }
}

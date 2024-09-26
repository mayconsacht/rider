using EventBus.Events;

namespace Ride.Application.IntegrationEvents;

public interface IRideIntegrationEventService
{
    Task PublishThroughEventBusAsync(IntegrationEvent evt);
}
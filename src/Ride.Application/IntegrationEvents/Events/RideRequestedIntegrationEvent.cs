using EventBus.Events;

namespace Ride.Application.IntegrationEvents.Events;

public record RideRequestedIntegrationEvent(Guid RideId, Guid PassengerId) : IntegrationEvent;

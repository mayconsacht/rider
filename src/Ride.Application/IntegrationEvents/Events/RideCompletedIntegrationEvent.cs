using EventBus.Events;

namespace Ride.Application.IntegrationEvents.Events;

public record RideCompletedIntegrationEvent(Guid RideId, double Fare) : IntegrationEvent;
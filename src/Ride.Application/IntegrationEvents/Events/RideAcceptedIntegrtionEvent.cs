using EventBus.Events;

namespace Ride.Application.IntegrationEvents.Events;

public record RideAcceptedIntegrtionEvent(Guid RideId, string DriverName) : IntegrationEvent;
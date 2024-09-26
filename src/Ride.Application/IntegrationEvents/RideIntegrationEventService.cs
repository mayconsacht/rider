using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.Extensions.Logging;

namespace Ride.Application.IntegrationEvents;

public class RideIntegrationEventService(ILogger<RideIntegrationEventService> logger,
    IEventBus eventBus) : IRideIntegrationEventService
{
    private volatile bool disposedValue;
    
    public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
    {
        try
        {
            logger.LogInformation("Publishing integration event: {IntegrationEventId_published} - ({@IntegrationEvent})", evt.Id, evt);
            await eventBus.PublishAsync(evt);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", evt.Id, evt);
        }
    }
}
using Ride.Infrastructure.Database;
using Ride.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Ride.Application.IntegrationEvents;

namespace Ride.Tests.Integration;

public abstract class TestBase : IDisposable
{
    protected RideDbContext Context;
    protected RideRepository RideRepository;
    protected PositionRepository PositionRepository;
    protected IRideIntegrationEventService RideIntegrationEventService;
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<RideDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        Context = new RideDbContext(options);
        RideIntegrationEventService = Mock.Of<IRideIntegrationEventService>();
        CreateRepositories();
    }
    
    private void CreateRepositories()
    {
        RideRepository = new RideRepository(Context);    
        PositionRepository = new PositionRepository(Context);
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}
using Ride.Infrastructure.Database;
using Ride.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ride.Tests.Integration;

public abstract class TestBase : IDisposable
{
    protected RideDbContext Context;
    protected RideRepository RideRepository;
    protected PositionRepository PositionRepository;
    
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<RideDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        Context = new RideDbContext(options);
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
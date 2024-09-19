using Microsoft.EntityFrameworkCore;
using Rider.Infrastructure.Database;
using Rider.Infrastructure.Repositories;

namespace Rider.Tests.Integration;

public abstract class TestBase : IDisposable
{
    protected readonly RiderDbContext Context;
    protected AccountRepository AccountRepository;

    protected TestBase()
    {
        var options = new DbContextOptionsBuilder<RiderDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        Context = new RiderDbContext(options);
        CreateRepositories();
    }

    private void CreateRepositories()
    {
        AccountRepository = new AccountRepository(Context);    
    }
    
    public void Dispose()
    {
        Context.Dispose();
    }
}
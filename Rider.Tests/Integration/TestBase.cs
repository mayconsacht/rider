using Microsoft.EntityFrameworkCore;
using Rider.Infrastructure.Database;
using Rider.Infrastructure.Repositories;

namespace Rider.Tests.Integration;

public abstract class TestBase : IDisposable
{
    protected AccountDbContext Context;
    protected AccountRepository AccountRepository;
    
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AccountDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        Context = new AccountDbContext(options);
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
using Microsoft.EntityFrameworkCore;
using Rider.Infrastructure.Database;
using Rider.Infrastructure.Repositories;

public abstract class TestBase : IDisposable
{
    protected readonly RiderDbContext _context;
    protected AccountRepository _accountRepository;

    protected TestBase()
    {
        var options = new DbContextOptionsBuilder<RiderDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new RiderDbContext(options);
        CreateRepositories();
    }

    protected void CreateRepositories()
    {
        _accountRepository = new AccountRepository(_context);    
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}
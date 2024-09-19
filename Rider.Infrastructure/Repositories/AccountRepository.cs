using Microsoft.EntityFrameworkCore;
using Rider.Application.Repositories;
using Rider.Domain.Entities;
using Rider.Infrastructure.Database;

namespace Rider.Infrastructure.Repositories;

public class AccountRepository(RiderDbContext db) : IAccountRepository
{
    public async Task<Account?> GetByEmail(string email)
    {
        return await db.Accounts
            .Where(account => account.Email.Value.Equals(email))
            .FirstOrDefaultAsync();
    }

    public async Task<Account?> GetById(Guid accountId)
    {
        return await db.Accounts
            .Where(account => account.Id.Equals(accountId))
            .FirstOrDefaultAsync();
    }

    public async Task Save(Account account)
    {
        await db.Accounts.AddAsync(account);
        await db.SaveChangesAsync();
    }

    public async Task<List<Account?>> List()
    {
        return await db.Accounts.ToListAsync();
    }
}
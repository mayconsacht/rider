using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Rider.Domain.Entities;
using Rider.Infrastructure.Database;

namespace Rider.Infrastructure.Repositories;

public class AccountRepository(RiderDbContext db) : IAccountRepository
{
    public async Task<Account?> GetByEmail(string email)
    {
        return await db.Accounts
            .Where(account => account.Email.Equals(email))
            .FirstOrDefaultAsync();
    }

    public async Task<Account> GetById(Guid accountId)
    {
        return await db.Accounts
            .Where(account => account.Id.Equals(accountId))
            .FirstOrDefaultAsync();
    }

    public async Task Save(Account account)
    {
        await db.Accounts.AddAsync(account);
    }

    public async Task<List<Account?>> List()
    {
        return await db.Accounts.ToListAsync();
    }
}
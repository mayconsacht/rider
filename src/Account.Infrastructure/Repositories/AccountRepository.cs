using Microsoft.EntityFrameworkCore;
using Account.Application.Repositories;
using Account.Infrastructure.Database;
using AccountEntity = Account.Domain.Entities.Account;

namespace Account.Infrastructure.Repositories;

public class AccountRepository(AccountDbContext db) : IAccountRepository
{
    public async Task<AccountEntity?> GetByEmail(string email)
    {
        return await db.Accounts
            .Where(account => account.Email.Value.Equals(email))
            .FirstOrDefaultAsync();
    }

    public async Task<AccountEntity?> GetById(Guid accountId)
    {
        return await db.Accounts
            .Where(account => account.Id.Equals(accountId))
            .FirstOrDefaultAsync();
    }

    public async Task Save(AccountEntity account)
    {
        await db.Accounts.AddAsync(account);
        await db.SaveChangesAsync();
    }

    public async Task<List<AccountEntity?>> List()
    {
        return await db.Accounts.ToListAsync();
    }
}
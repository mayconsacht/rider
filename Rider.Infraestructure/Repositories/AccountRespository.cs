using Application.Repositories;
using Rider.Domain.Entities;
using Rider.Infraestructure.Database;

namespace Rider.Infraestructure.Repositories;

public class AccountRespository(RiderDbContext db) : IAccountRepository
{
    public Task<Account> GetByEmail(string email)
    {
        db
    }

    public Task<Account> GetById(Guid accountId)
    {
        throw new NotImplementedException();
    }

    public Task Save(Account account)
    {
        throw new NotImplementedException();
    }

    public Task<List<Account>> List()
    {
        throw new NotImplementedException();
    }
}
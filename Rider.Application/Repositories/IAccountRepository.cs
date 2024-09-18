using Rider.Domain.Entities;

namespace Application.Repositories;

public interface IAccountRepository
{
    Task<Account> GetByEmail(string email);
    Task<Account> GetById(Guid accountId);
    Task Save(Account account);
    Task<List<Account>> List();
}

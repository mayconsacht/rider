using AccountEntity = Account.Domain.Entities.Account;

namespace Account.Application.Repositories;

public interface IAccountRepository
{
    Task<AccountEntity?> GetByEmail(string email);
    Task<AccountEntity?> GetById(Guid accountId);
    Task Save(AccountEntity account);
    Task<List<AccountEntity?>> List();
}

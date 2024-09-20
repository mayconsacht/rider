using Account.Application.Repositories;
using BuildingBlocks.Shared.DTO.Account;
using Microsoft.Extensions.Logging;
using AccountEntity = Account.Domain.Entities.Account;

namespace Account.Application.UseCases.Account;

public class GetAccount(IAccountRepository accountRepository, ILogger<GetAccount> logger) : IUseCase<Guid, Task<AccountDto?>>
{
    public async Task<AccountDto?> Execute(Guid id)
    {
        AccountEntity? account = await accountRepository.GetById(id);
        if (account == null)
        {
            logger.LogWarning("Account not found");
            return null;
        }
        return new AccountDto()
        {
            Id = account.Id,
            Name = account.Name.Value,
            Email = account.Email.Value,
            CarPlate = account.CarPlate,
            IsDriver = account.IsDriver,
            IsPassenger = account.IsPassenger
        };
    }
}
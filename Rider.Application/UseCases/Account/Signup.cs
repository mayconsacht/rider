using Rider.Application.Repositories;
using Rider.CrossCutting.DTO.Account;
using Rider.Domain.Exceptions;
using AccountEntity = Rider.Domain.Entities.Account;

namespace Rider.Application.UseCases.Account;

public class Signup(IAccountRepository accountRepository) : IUseCase<AccountNoIdDto, Task<Guid>>
{
    public async Task<Guid> Execute(AccountNoIdDto input)
    {
        AccountEntity? existingAccount = await accountRepository.GetByEmail(input.Email);
        if (existingAccount != null) 
        {
            throw new RiderDomainException($"Email {input.Email} already exists");
        }
        AccountEntity account = AccountEntity.Create(input.Name, input.Email, input.CarPlate, input.IsDriver, input.IsPassenger);
        await accountRepository.Save(account);
        return account.Id;
    }
}
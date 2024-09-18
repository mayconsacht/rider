using Application.Repositories;
using Rider.CrossCutting.DTO.Account;
using AccountEntity = Rider.Domain.Entities.Account;

namespace Application.UseCases.Account;

public class Signup(IAccountRepository accountRepository) : IUseCase<AccountNoIdDto, Task<Guid>>
{
    public async Task<Guid> Execute(AccountNoIdDto input)
    {
        AccountEntity existingAccount = await accountRepository.GetByEmail(input.Email);
        if (existingAccount != null) 
        {
            throw new Exception($"Email {input.Email} already exists");
        }
        AccountEntity account = AccountEntity.Create(input.Name, input.Email, input.CarPlate, input.IsDriver, input.IsPassenger);
        await accountRepository.Save(account);
        return account.Id;
    }
}
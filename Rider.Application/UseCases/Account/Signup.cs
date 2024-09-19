using Rider.Application.Gateways;
using Rider.Application.Repositories;
using Rider.CrossCutting.DTO.Account;
using Rider.Domain.Exceptions;
using AccountEntity = Rider.Domain.Entities.Account;

namespace Rider.Application.UseCases.Account;

// Soon to be changed to commands
public class Signup(IAccountRepository accountRepository, IMailerGateway mailer) : IUseCase<AccountNoIdDto, Task<Guid?>>
{
    public virtual async Task<Guid?> Execute(AccountNoIdDto input)
    {
        AccountEntity? existingAccount = await accountRepository.GetByEmail(input.Email);
        if (existingAccount != null)
        {
            throw new AccountDomainException($"Email {input.Email} already exists");
        }

        AccountEntity account = AccountEntity.Create(input.Name, input.Email, input.CarPlate, input.IsDriver,
            input.IsPassenger);
        await accountRepository.Save(account);
        mailer.Send(account.Email.Value, "Welcome!", "Account created");
        
        return account.Id;
    }
}
using Account.Application.Gateways;
using Account.Application.Repositories;
using Microsoft.Extensions.Logging;
using BuildingBlocks.Shared.DTO.Account;
using AccountEntity = Account.Domain.Entities.Account;

namespace Account.Application.UseCases.Account;

public class Signup(IAccountRepository accountRepository, IMailerGateway mailer, ILogger<Signup> logger) : IUseCase<AccountNoIdDto, Task<Guid?>>
{
    public virtual async Task<Guid?> Execute(AccountNoIdDto input)
    {
        AccountEntity? existingAccount = await accountRepository.GetByEmail(input.Email);
        if (existingAccount != null)
        {
            logger.LogWarning($"Email {input.Email} already exists");
            return null;
        }

        try
        {
            AccountEntity account = AccountEntity.Create(input.Name, input.Email, input.CarPlate, input.IsDriver,
                input.IsPassenger);
            logger.LogInformation("Creating Account - AccountId: {AccountId}", account.Id);
            await accountRepository.Save(account);
            mailer.Send(account.Email.Value, "Welcome!", "Account created");
            return account.Id;
        }
        catch (Exception e)
        {
            logger.LogWarning($"Failed to create account: {e.Message}");
            return null;
        }
    }
}
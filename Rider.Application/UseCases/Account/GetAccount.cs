using Application.Repositories;
using Rider.CrossCutting.DTO.Account;

using AccountEntity = Rider.Domain.Entities.Account;

namespace Application.UseCases.Account;

public class GetAccount(IAccountRepository accountRepository) : IUseCase<Guid, Task<AccountDto>>
{
    public async Task<AccountDto> Execute(Guid id)
    {
        AccountEntity account = await accountRepository.GetById(id);
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
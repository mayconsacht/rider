using Shared.DTO.Account;

namespace Ride.Application.Gateways;

public interface IAccountGateway
{
    Task<Guid?> Signup(AccountDto account);
    Task<AccountDto?> GetAccountById(Guid accountId);
}
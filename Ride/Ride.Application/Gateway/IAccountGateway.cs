using BuildingBlocks.Shared.DTO.Account;

namespace Ride.Application.Gateway;

public interface IAccountGateway
{
    Task<Guid?> Signup(AccountDto account);
    Task<AccountDto?> GetAccountById(Guid accountId);
}
using BuildingBlocks.Shared.DTO.Account;
using BuildingBlocks.Shared.Infrastructure;
using Ride.Application.Gateways;

namespace Ride.Infrastructure.Gateways;

public class AccountGatewayHttp(HttpService http, IConfiguration configuration, ILogger<AccountGatewayHttp> logger)
    : IAccountGateway
{
    private readonly string? _baseUrl = configuration.GetValue<string>("AccountUri");

    public async Task<Guid?> Signup(AccountDto account)
    {
        try
        {
            return await http.PostAsync<Guid?>(_baseUrl, account);
        }
        catch (HttpRequestException e)
        {
            logger.LogWarning($"Failed to call API: {e.Message}");
        }
        return null;
    }

    public async Task<AccountDto?> GetAccountById(Guid accountId)
    {
        return await http.GetAsync<AccountDto>($"{_baseUrl}/{accountId}");
    }
}
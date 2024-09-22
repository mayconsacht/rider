using BuildingBlocks.Shared.DTO.Account;
using BuildingBlocks.Shared.Infrastructure;
using Ride.Application.Gateways;

namespace Ride.Infrastructure.Gateways;

public class AccountGatewayHttp : IAccountGateway
{
    private readonly HttpService _http;
    private readonly string _baseUrl;
    private readonly ILogger<AccountGatewayHttp> _logger;
    
    public AccountGatewayHttp(HttpService http, IConfiguration configuration, ILogger<AccountGatewayHttp> logger)
    {
        _http = http;
        _baseUrl = configuration.GetValue<string>("AccountUri");
        _logger = logger;
    }
    
    public async Task<Guid?> Signup(AccountDto account)
    {
        try
        {
            return await _http.PostAsync<Guid?>(_baseUrl, account);
        }
        catch (HttpRequestException e)
        {
            _logger.LogWarning($"Failed to call API: {e.Message}");
        }
        return null;
    }

    public async Task<AccountDto?> GetAccountById(Guid accountId)
    {
        return await _http.GetAsync<AccountDto>($"{_baseUrl}/{accountId}");
    }
}
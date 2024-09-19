using Rider.Application.UseCases.Account;

namespace Rider.Infrastructure.Apis;

public class AccountServices(GetAccount getAccount, Signup signup, ILogger<AccountServices> logger)
{
    public GetAccount GetAccount { get; } = getAccount;
    public Signup Signup { get; } = signup;
    public ILogger<AccountServices> Logger { get; } = logger;
}

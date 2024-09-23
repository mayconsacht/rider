using Account.Application.UseCases.Account;
using Account.Infrastructure.Apis;
using Account.Infrastructure.Gateways;
using Account.Tests.Mocks.Account;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Moq;

namespace Account.Tests.Integration.Infrastructure;

[TestFixture]
public class AccountApiTest : TestBase
{
    private ILogger<AccountServices> _loggerMock;
    
    [SetUp]
    public void Setup()
    {
        _loggerMock = Mock.Of<ILogger<AccountServices>>();
    }
    
    [Test]
    public async Task CreateAccountAsync_ShouldReturnCreated_WhenAccountIsValid()
    {
        // Arrange
        var account = AccountMock.CreateNoIdDto();
        var mailer = new MailerGatewayFake();
        var loggerGetAccount = Mock.Of<ILogger<GetAccount>>();
        var loggerSignup = Mock.Of<ILogger<Signup>>();
        var getAccount = new GetAccount(AccountRepository, loggerGetAccount);
        var service = new AccountServices(getAccount, new Signup(AccountRepository, mailer, loggerSignup), _loggerMock);
        
        // Act
        var result = await AccountApi.CreateAccountAsync(account, service);
        
        // Assert
        Assert.IsInstanceOf<Created<Guid?>>(result.Result);
        var createdResult = result.Result as Created<Guid?>;
        var createdId = createdResult.Value;
        Assert.IsNotNull(createdId);
        
        var newAccount = await getAccount.Execute(createdId ?? Guid.Empty);
        Assert.IsNotNull(newAccount);
        Assert.That(newAccount.Name, Is.EqualTo(account.Name));
        Assert.That(newAccount.Email, Is.EqualTo(account.Email));
        Assert.That(newAccount.CarPlate, Is.EqualTo(account.CarPlate));
        Assert.That(newAccount.IsDriver, Is.EqualTo(account.IsDriver));
        Assert.That(newAccount.IsPassenger, Is.EqualTo(account.IsPassenger));
    }
}
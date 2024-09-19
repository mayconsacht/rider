using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Moq;
using Rider.Application.UseCases.Account;
using Rider.Infrastructure.Apis;
using Rider.Tests.Mocks.Account;

namespace Rider.Tests.Integration.Infrastructure;

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
        var service = new AccountServices(new GetAccount(AccountRepository), new Signup(AccountRepository), _loggerMock);
        
        // Act
        var result = await AccountApi.CreateAccountAsync(account, service);

        //Assert
        Assert.IsInstanceOf<Created<Guid?>>(result.Result);
    }
}
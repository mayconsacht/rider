using Account.Application.UseCases.Account;
using Account.Infrastructure.Gateways;
using Account.Tests.Mocks.Account;
using Shared.DTO.Account;
using Microsoft.Extensions.Logging;
using Moq;

namespace Account.Tests.Integration.Application.UseCases;

[TestFixture]
public class SignupTest : TestBase
{
    private Signup _signup;
    private GetAccount _getAccount;
    private Mock<ILogger<Signup>> _loggerSignup;
    
    [SetUp]
    public void Setup()
    {
        var mailer = new MailerGatewayFake();
        var loggerGetAccount = Mock.Of<ILogger<GetAccount>>();
        _loggerSignup = new Mock<ILogger<Signup>>();
        _getAccount = new GetAccount(AccountRepository, loggerGetAccount);
        _signup = new Signup(AccountRepository, mailer, _loggerSignup.Object);
    }
    
    [Test]
    public async Task SignupTest_ShouldCreateNewAccount()
    {
        //Arrange
        var account = AccountMock.CreateNoIdDto();
        
        //Act
        var accountId = await _signup.Execute(account);
        Assert.IsNotNull(accountId);
        var newAccount = await _getAccount.Execute(accountId ?? Guid.Empty);
        
        //Assert
        Assert.IsNotNull(newAccount);
        Assert.That(newAccount.Name, Is.EqualTo(account.Name));
        Assert.That(newAccount.Email, Is.EqualTo(account.Email));
        Assert.That(newAccount.CarPlate, Is.EqualTo(account.CarPlate));
        Assert.That(newAccount.IsDriver, Is.EqualTo(account.IsDriver));
        Assert.That(newAccount.IsPassenger, Is.EqualTo(account.IsPassenger));
    }
    
    [Test]
    public async Task SignupTest_WhenEmailAlreadyRegistered_ShouldThrowException()
    {
        //Arrange
        var account = new AccountNoIdDto
        {
            Name = "John Doe",
            Email = "john.doe@gmail.com",
            CarPlate = "MBO8765",
            IsDriver = true,
            IsPassenger = true
        };
        
        //Act
        var accountId = await _signup.Execute(account);
        Assert.IsNotNull(accountId);
        var newAccount = await _getAccount.Execute(accountId ?? Guid.Empty);
        Assert.IsNotNull(newAccount);
        var result = await _signup.Execute(account);
        
        //Assert
        Assert.IsNull(result);
        _loggerSignup.Verify(
            x => x.Log(
                It.Is<LogLevel>(l => l == LogLevel.Warning),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == $"Email {account.Email} already exists"),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)), 
            Times.Once); 
    }
}
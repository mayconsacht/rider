using Rider.Application.UseCases.Account;
using Rider.CrossCutting.DTO.Account;
using Rider.Domain.Exceptions;
using Rider.Tests.Mocks.Account;

namespace Rider.Tests.Integration.Application.UseCases;

[TestFixture]
public class SignupTest : TestBase
{
    private Signup _signup;
    private GetAccount _getAccount;
    
    [SetUp]
    public void Setup()
    {
        _getAccount = new GetAccount(AccountRepository);
        _signup = new Signup(AccountRepository);
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
        
        //Assert
        Assert.IsNotNull(newAccount);
        var ex = Assert.ThrowsAsync<RiderDomainException>(async () => await _signup.Execute(account));
        Assert.That(ex.Message, Is.EqualTo($"Email {account.Email} already exists"));
    }
}
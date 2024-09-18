using Application.UseCases.Account;
using Rider.CrossCutting.DTO.Account;

namespace Rider.Tests.Application.UseCases;

public class SignupTest : TestBase
{
    private Signup _signup;
    private GetAccount _getAccount;
    
    [SetUp]
    public void SetupTest()
    {
        _getAccount = new GetAccount(_accountRepository);
        _signup = new Signup(_accountRepository);
    }
    
    [Test]
    public async Task SignupTest_ShouldCreateNewAccount()
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
        var newAccount = await _getAccount.Execute(accountId);
        
        //Assert
        Assert.That(newAccount.Name, Is.EqualTo(account.Name));
        Assert.That(newAccount.Email, Is.EqualTo(account.Email));
        Assert.That(newAccount.CarPlate, Is.EqualTo(account.CarPlate));
        Assert.That(newAccount.IsDriver, Is.EqualTo(account.IsDriver));
        Assert.That(newAccount.IsPassenger, Is.EqualTo(account.IsPassenger));
    }
}
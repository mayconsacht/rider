using Rider.Domain.Entities;
using Rider.Domain.Exceptions;

namespace Rider.Tests.Domain.Entities;

public class AccountTest
{
    [Test]
    public void Account_ShouldBeCreated()
    {
        //Arrange
        const string inputName = "John Doe";
        const string inputEmail = "john.doe@mail.com";
        const string inputCarPlate = "MBO2345";
        
        //Act
        var accountCreated = Account.Create(inputName, inputEmail, inputCarPlate, false, true);
        
        //Assert
        Assert.That(accountCreated.Name.Value, Is.EqualTo(inputName));
        Assert.That(accountCreated.Email.Value, Is.EqualTo(inputEmail));
        Assert.That(accountCreated.CarPlate, Is.EqualTo(inputCarPlate));
        Assert.That(accountCreated.IsPassenger, Is.False);
        Assert.That(accountCreated.IsDriver, Is.True);
    }
    
    [Test]
    public void Account_WhenInvalidName_ShouldThrowException()
    {
        //Assert
        var ex = Assert.Throws<AccountDomainException>(CreateAccount);
        Assert.That(ex.Message, Is.EqualTo("Invalid account name."));
        return;

        //Arrange
        void CreateAccount() => Account.Create("John", "john@mail.com", "MBO2345", false, true);
    }
    
    [Test]
    public void Account_WhenInvalidEmail_ShouldThrowException()
    {
        //Assert
        var ex = Assert.Throws<AccountDomainException>(CreateAccount);
        Assert.That(ex.Message, Is.EqualTo("Invalid account email format."));
        return;

        //Arrange
        void CreateAccount() => Account.Create("John Doe", "john@", "MBO2345", false, true);
    }
    
    [Test]
    public void Account_WhenInvalidCarPlate_ShouldThrowException()
    {
        //Assert
        var ex = Assert.Throws<AccountDomainException>(CreateAccount);
        Assert.That(ex.Message, Is.EqualTo("Invalid car plate."));
        return;

        //Arrange
        void CreateAccount() => Account.Create("John Doe", "john@mail.com", "", false, true);
    }
}
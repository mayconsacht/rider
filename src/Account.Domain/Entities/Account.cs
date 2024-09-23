using Account.Domain.Exceptions;
using Account.Domain.ValueObjects;
using Shared.Domain;

namespace Account.Domain.Entities;

public sealed class Account : Entity, IAggregateRoot
{
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public string CarPlate { get; private set; }
    public bool IsPassenger { get; private set; }
    public bool IsDriver { get; private set; }

    public Account() {}
    
    public Account(Guid id, string name, string email, string carPlate, bool isPassenger, bool isDriver)
    {
        Id = id;
        Name = new Name(name);
        Email = new Email(email);
        IsPassenger = isPassenger;
        IsDriver = isDriver;
        if (isDriver && string.IsNullOrEmpty(carPlate))
        {
            throw new AccountDomainException("Invalid car plate.");
        }
        CarPlate = carPlate;
    }
    
    public static Account Create(string name, string email, string carPlate, bool isPassenger, bool isDriver)
    {
        return new Account(Guid.NewGuid(), name, email, carPlate, isPassenger, isDriver);
    }
}

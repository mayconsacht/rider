using Rider.Domain.ValueObjects;

namespace Rider.Domain.Entities;

public class Account
{
    public Guid Id { get; private set; }
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public string CarPlate { get; private set; }
    public bool IsPassenger { get; private set; }
    public bool IsDriver { get; private set; }

    public Account(Guid id, string name, string email, string carPlate, bool isPassenger, bool isDriver)
    {
        Id = id;
        Name = new Name(name);
        Email = new Email(email);
        IsPassenger = isPassenger;
        IsDriver = isDriver;
        if (isDriver && string.IsNullOrEmpty(carPlate))
        {
            throw new ArgumentException("Invalid car plate.");
        }
        CarPlate = carPlate;
    }
    
    public static Account Create(string name, string email, string carPlate, bool isPassenger, bool isDriver)
    {
        return new Account(new Guid(), name, email, carPlate, isPassenger, isDriver);
    }
}

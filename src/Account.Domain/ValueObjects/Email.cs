using System.Text.RegularExpressions;
using Account.Domain.Exceptions;

namespace Account.Domain.ValueObjects;

public record Email
{
    public string Value { get; private set; }   
    
    public Email(string value)
    {
        if (string.IsNullOrEmpty(value) || !Regex.IsMatch(value, @"^(.+)@(.+)$"))
        {
            throw new AccountDomainException("Invalid account email format.");
        }
        Value = value;
    }
}
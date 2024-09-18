using System.Text.RegularExpressions;
using Rider.Domain.Exceptions;

namespace Rider.Domain.ValueObjects;

public record Email
{
    public string Value { get; private set; }   
    
    public Email(string value)
    {
        if (string.IsNullOrEmpty(value) || !Regex.IsMatch(value, @"^(.+)@(.+)$"))
        {
            throw new RiderDomainException("Invalid account email format.");
        }
        Value = value;
    }
}
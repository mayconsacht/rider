

using System.Text.RegularExpressions;

namespace Rider.Domain.ValueObjects;

public record Name
{
    public string Value { get; private set; }   
    
    public Name(string value)
    {
        if (string.IsNullOrEmpty(value) || !Regex.IsMatch(value, @"^[a-zA-Z]+ [a-zA-Z]+$"))
        {
            throw new ArgumentException("Invalid account name.");
        }
        Value = value;
    }
}


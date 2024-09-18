using System.Text.RegularExpressions;

namespace Rider.Domain.ValueObjects;

public record Email
{
    public string Value { get; private set; }   
    
    public Email(string value)
    {
        if (string.IsNullOrEmpty(value) || !Regex.IsMatch(value, @"^(.+)@(.+)$"))
        {
            throw new ArgumentException("Invalid account email format.");
        }
        Value = value;
    }
}
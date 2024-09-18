namespace Rider.Domain.Exceptions;

public class RiderDomainException : Exception
{
    public RiderDomainException()
    {
    }

    public RiderDomainException(string message)
        : base(message)
    {
    }

    public RiderDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
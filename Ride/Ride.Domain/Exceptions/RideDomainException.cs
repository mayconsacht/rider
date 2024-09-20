namespace Ride.Domain.Exceptions;

public class RideDomainException : Exception
{
    public RideDomainException()
    {
    }

    public RideDomainException(string message)
        : base(message)
    {
    }

    public RideDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
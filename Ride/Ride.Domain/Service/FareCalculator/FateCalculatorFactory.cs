using Ride.Domain.Exceptions;

namespace Ride.Domain.Service.FareCalculator;

public class FareCalculatorFactory
{
    public static IFareCalculator Create(DateTime date)
    {
        if (date.Day == 0) return new SundayFare();
        if (date.Hour is > 18 or < 8) return new OvernightFare();
        if (date.Hour is >= 8 and <= 18) return new NormalFare();
        throw new RideDomainException("Invalid date");
    }
}
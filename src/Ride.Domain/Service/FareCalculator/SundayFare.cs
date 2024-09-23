namespace Ride.Domain.Service.FareCalculator;

public class SundayFare : IFareCalculator
{
    public double Calculate(double distance)
    {
        return distance * 5;
    }
}

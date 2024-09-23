namespace Ride.Domain.Service.FareCalculator;

public class NormalFare : IFareCalculator
{
    public double Calculate(double distance)
    {
        return distance * 2.1;
    }
}
namespace Ride.Domain.Service.FareCalculator;

public class OvernightFare : IFareCalculator
{
    public double Calculate(double distance)
    {
        return distance * 3.9;
    }
}
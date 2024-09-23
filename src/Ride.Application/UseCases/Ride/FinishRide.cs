using Shared.Application;
using Ride.Application.Repositories;

namespace Ride.Application.UseCases.Ride;

public class FinishRide(IRideRepository rideRepository) : IUseCase<Guid, Task<Guid?>>
{
    public async Task<Guid?> Execute(Guid rideId)
    {
        var ride = await rideRepository.GetRideById(rideId);
        ride.Finish();
        await rideRepository.UpdateRide(ride);
        return ride.Id;
    }
}

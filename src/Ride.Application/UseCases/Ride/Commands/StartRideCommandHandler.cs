using MediatR;
using Ride.Application.Repositories;

namespace Ride.Application.UseCases.Ride.Commands;

public class StartRideCommandHandler(IRideRepository rideRepository) : IRequestHandler<StartRideCommand, Guid?>
{
    public async Task<Guid?> Handle(StartRideCommand request, CancellationToken cancellationToken)
    {
        var ride = await rideRepository.GetRideById(request.RideId);
        ride.Start();
        await rideRepository.UpdateRide(ride);
        return ride.Id;
    }
}
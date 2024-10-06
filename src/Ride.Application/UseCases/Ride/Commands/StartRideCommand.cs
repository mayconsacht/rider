using MediatR;

namespace Ride.Application.UseCases.Ride.Commands;

public record StartRideCommand(Guid RideId) : IRequest<Guid?>;

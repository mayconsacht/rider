using MediatR;

namespace Ride.Application.UseCases.Ride.Commands;

public record AcceptRideCommand(Guid RideId, Guid DriverId) : IRequest<Guid?>;
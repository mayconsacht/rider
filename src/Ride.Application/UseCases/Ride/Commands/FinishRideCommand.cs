using MediatR;

namespace Ride.Application.UseCases.Ride.Commands;

public record FinishRideCommand(Guid RideId) : IRequest<Guid>;

using MediatR;

namespace Ride.Application.UseCases.Ride.Commands;

public record RequestRideCommand(Guid PassengerId, double FromLatitude, double FromLongitude, 
  double ToLatitude, double ToLongitude) : IRequest<Guid?>;

using Ride.Application.UseCases.Ride.Queries.ViewModel;

namespace Ride.Application.UseCases.Ride.Queries;

public interface IRideQueries
{
  Task<RideView> GetRide(Guid request);
}


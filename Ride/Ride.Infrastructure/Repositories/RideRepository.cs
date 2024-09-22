using Microsoft.EntityFrameworkCore;
using Ride.Application.Repositories;
using Ride.Infrastructure.Database;

namespace Ride.Infrastructure.Repositories;

public class RideRepository(RideDbContext db) : IRideRepository
{
    public async Task<Guid?> Save(Domain.Entities.Ride? ride)
    {
        var savedRide = await db.Rides.AddAsync(ride);
        await db.SaveChangesAsync();
        return savedRide.Entity.Id;
    }

    public async Task<Domain.Entities.Ride?> GetRideById(Guid id)
    {
        return await db.Rides
            .Where(ride => ride.Id.Equals(id))
            .FirstOrDefaultAsync();
    }

    public async Task<bool> HasActiveRideByPassengerId(Guid passengerId)
    {
        return await db.Rides
            .Where(ride => ride.PassengerId.Equals(passengerId))
            .AnyAsync();
    }

    public async Task<bool> HasActiveRideByDriverId(Guid driverId)
    {
        return await db.Rides
            .Where(ride => ride.DriverId.Equals(driverId))
            .AnyAsync();
    }

    public async Task<Domain.Entities.Ride> UpdateRide(Domain.Entities.Ride ride)
    {
        db.Rides.Update(ride);
        await db.SaveChangesAsync();
        return ride;
    }
}
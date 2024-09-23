using Microsoft.EntityFrameworkCore;
using Ride.Application.Repositories;
using Ride.Domain.Entities;
using Ride.Infrastructure.Database;

namespace Ride.Infrastructure.Repositories;

public class PositionRepository(RideDbContext db) : IPositionRepository
{
    public async Task<Guid?> SavePosition(Position position)
    {
        var savedPosition = await db.Positions.AddAsync(position);
        await db.SaveChangesAsync();
        return savedPosition.Entity.Id;
    }

    public async Task<Position?> GetLastPositionFromRideId(Guid id)
    {
        return await db.Positions
            .Where(position => position.RideId.Equals(id))
            .OrderByDescending(p => p.Date)
            .LastOrDefaultAsync();
    }
}
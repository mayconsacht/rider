using Microsoft.EntityFrameworkCore;
using Ride.Domain.Entities;

namespace Ride.Infrastructure.Database;

public class RideDbContext : DbContext
{
    public DbSet<Domain.Entities.Ride?> Rides { get; set; }
    public DbSet<Position?> Positions { get; set; }
}

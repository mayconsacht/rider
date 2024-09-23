using Microsoft.EntityFrameworkCore;
using Ride.Domain.Entities;
using Ride.Infrastructure.Database.EntityConfigurations;

namespace Ride.Infrastructure.Database;

public class RideDbContext(DbContextOptions<RideDbContext> options) : DbContext(options)
{
    public DbSet<Domain.Entities.Ride> Rides { get; init; }
    public DbSet<Position> Positions { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RideEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PositionEntityTypeConfiguration());
    }
}

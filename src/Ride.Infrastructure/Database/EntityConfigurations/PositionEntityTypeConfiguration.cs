using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ride.Domain.Entities;

namespace Ride.Infrastructure.Database.EntityConfigurations;

public class PositionEntityTypeConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("positions");
        builder.HasKey(position => position.Id);
        builder.OwnsOne(position => position.Coordinate, coordinate =>
        {
            coordinate.Property(coordinateCoordinate => coordinateCoordinate.Latitude).HasColumnName("latitude");
            coordinate.Property(coordinateCoordinate => coordinateCoordinate.Longitude).HasColumnName("longitude");
        });
    }
}
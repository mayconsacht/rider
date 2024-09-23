using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ride.Domain.ValueObject;

namespace Ride.Infrastructure.Database.EntityConfigurations;

public class RideEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.Ride>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Ride> builder)
    {
        builder.ToTable("rides");
        builder.HasKey(r => r.Id);
        builder.OwnsOne(r => r.To, nb =>
        {
            nb.Property(n => n.Longitude).HasColumnName("to_longitude");
            nb.Property(n => n.Latitude).HasColumnName("to_latitude");
        });
        builder.OwnsOne(r => r.From, nb =>
        {
            nb.Property(n => n.Longitude).HasColumnName("from_longitude");
            nb.Property(n => n.Latitude).HasColumnName("from_latitude");
        });
    }
}
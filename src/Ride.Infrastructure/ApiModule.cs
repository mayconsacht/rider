using Ride.Infrastructure.Database;

namespace Ride.Infrastructure;

public static class ApiModule
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<RideDbContext>("ridedb");

        // Configure MediatR
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
        });
    }
}
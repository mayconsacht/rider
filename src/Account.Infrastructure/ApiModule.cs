using Account.Infrastructure.Database;

namespace Account.Infrastructure;

public static class ApiModule
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<AccountDbContext>("accountdb");
    }
}
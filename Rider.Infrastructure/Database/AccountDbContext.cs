using Microsoft.EntityFrameworkCore;
using Rider.Domain.Entities;
using Rider.Infrastructure.Database.EntityConfigurations;

namespace Rider.Infrastructure.Database;

public class AccountDbContext(DbContextOptions<AccountDbContext> options) : DbContext(options)
{
    public DbSet<Account?> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new AccountEntityTypeConfiguration());
    }
}

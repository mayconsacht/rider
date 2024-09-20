using Account.Infrastructure.Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using AccountEntity = Account.Domain.Entities.Account;

namespace Account.Infrastructure.Database;

public class AccountDbContext(DbContextOptions<AccountDbContext> options) : DbContext(options)
{
    public DbSet<AccountEntity?> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new AccountEntityTypeConfiguration());
    }
}

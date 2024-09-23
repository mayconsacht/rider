using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Account.Domain.ValueObjects;
using AccountEntity = Account.Domain.Entities.Account;

namespace Account.Infrastructure.Database.EntityConfigurations;

public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<AccountEntity>
{
    public void Configure(EntityTypeBuilder<AccountEntity> builder)
    {
        builder.ToTable("accounts");
        builder.HasKey(b => b.Id);
        builder
            .Property(o => o.Email)
            .HasConversion(
                email => email.Value, 
                value => new Email(value)
            );
        builder
            .Property(o => o.Name)
            .HasConversion(
                email => email.Value, 
                value => new Name(value)
            );
    }
}
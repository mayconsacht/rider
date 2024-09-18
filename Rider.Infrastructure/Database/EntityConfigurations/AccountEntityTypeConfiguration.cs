using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rider.Domain.Entities;
using Rider.Domain.ValueObjects;

namespace Rider.Infrastructure.Database.EntityConfigurations;

public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
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
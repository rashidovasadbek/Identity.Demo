using Identity.Domain.Entities;
using Identity.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Persistence.EntityConfiguration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.Property(user => user.FirstName).HasMaxLength(64).IsRequired();
        builder.Property(user => user.LastName).HasMaxLength(64).IsRequired();
        builder.Property(user => user.PasswordHash).HasMaxLength(128).IsRequired();
        builder.Property(user => user.EmailAddress).HasMaxLength(128).IsRequired();
        
        builder
            .Property(user => user.Role)
            .HasConversion<string>()
            .HasDefaultValue(Role.Guest)
            .HasMaxLength(64)
            .IsRequired();
    }
}
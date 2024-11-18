using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Core.Domain.People.Aggregates;

namespace Nexus.Core.Infra.Data.Configurations.People;

public sealed class PersonPhoneConfiguration : IEntityTypeConfiguration<PersonPhone>
{
    public void Configure(EntityTypeBuilder<PersonPhone> builder)
    {
        builder.ToTable("PersonPhones", "Core");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.PersonId)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.CountryCode)
            .HasMaxLength(8)
            .IsRequired();

        builder.Property(p => p.Number)
            .HasMaxLength(16)
            .IsRequired();

        builder.HasIndex(p => new { p.PersonId, p.CountryCode, p.Number })
            .IsUnique();
    }
}
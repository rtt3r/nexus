using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Core.Domain.Persons.Aggregates;

namespace Nexus.Core.Infra.Data.Configurations.Persons;

public sealed class PhoneConfiguration : IEntityTypeConfiguration<Phone>
{
    public void Configure(EntityTypeBuilder<Phone> builder)
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
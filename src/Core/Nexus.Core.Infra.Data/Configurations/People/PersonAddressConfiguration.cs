using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Core.Domain.People.Aggregates;

namespace Nexus.Core.Infra.Data.Configurations.People;

public sealed class PersonAddressConfiguration : IEntityTypeConfiguration<PersonAddress>
{
    public void Configure(EntityTypeBuilder<PersonAddress> builder)
    {
        builder.ToTable("PersonAddresses");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.PersonId)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.PostalCode)
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(p => p.Street)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(p => p.Number)
            .HasMaxLength(16);

        builder.Property(p => p.Complement)
            .HasMaxLength(128);

        builder.Property(p => p.Neighborhood)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.City)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.State)
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(p => p.Country)
            .HasMaxLength(32)
            .IsRequired();
    }
}

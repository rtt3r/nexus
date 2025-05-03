using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Core.Domain.Persons.Aggregates;

namespace Nexus.Core.Infra.Data.Configurations.Persons;

public sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Persons", "Core");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.PersonType)
            .HasConversion<string>()
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(p => p.Name)
            .HasMaxLength(256)
            .IsRequired();

        builder.HasMany(p => p.Documents)
            .WithOne(p => p.Person)
            .HasForeignKey(p => p.PersonId);

        builder.HasMany(p => p.Contacts)
            .WithOne(p => p.Person)
            .HasForeignKey(p => p.PersonId);

        builder.HasMany(p => p.Addresses)
            .WithOne(p => p.Person)
            .HasForeignKey(p => p.PersonId);
    }
}

internal sealed class NaturalPersonConfiguration : IEntityTypeConfiguration<NaturalPerson>
{
    public void Configure(EntityTypeBuilder<NaturalPerson> builder)
    {
        builder.ToTable("NaturalPersons", "Core");

        builder.Property(p => p.Gender)
            .HasConversion<string>()
            .HasMaxLength(16);

        builder.Property(p => p.DateOfBirth);
    }
}

internal sealed class LegalEntityConfiguration : IEntityTypeConfiguration<LegalEntity>
{
    public void Configure(EntityTypeBuilder<LegalEntity> builder)
    {
        builder.ToTable("LegalEntities", "Core");

        builder.Property(p => p.BrandName)
            .HasMaxLength(256)
            .IsRequired();
    }
}

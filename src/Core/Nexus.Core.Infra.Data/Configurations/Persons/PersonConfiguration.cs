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

        builder.Property(p => p.Type)
            .HasMaxLength(7);

        builder.HasMany(p => p.Documents)
            .WithOne(p => p.Person)
            .HasForeignKey(p => p.PersonId);

        builder.HasMany(p => p.Phones)
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

        builder.ComplexProperty(o => o.Name, name =>
        {
            name.Property(p => p.FirstName)
                .HasColumnName(nameof(NaturalPersonName.FirstName))
                .HasMaxLength(128)
                .IsRequired();

            name.Property(p => p.LastName)
                .HasColumnName(nameof(NaturalPersonName.LastName))
                .HasMaxLength(128)
                .IsRequired();
        });
    }
}

internal sealed class LegalEntityConfiguration : IEntityTypeConfiguration<LegalEntity>
{
    public void Configure(EntityTypeBuilder<LegalEntity> builder)
    {
        builder.ToTable("LegalPersons", "Core");

        builder.ComplexProperty(o => o.Name, name =>
        {
            name.Property(p => p.BrandName)
                .HasColumnName(nameof(LegalEntityName.BrandName))
                .HasMaxLength(128)
                .IsRequired();

            name.Property(p => p.CompanyName)
                .HasColumnName(nameof(LegalEntityName.CompanyName))
                .HasMaxLength(256)
                .IsRequired();
        });
    }
}

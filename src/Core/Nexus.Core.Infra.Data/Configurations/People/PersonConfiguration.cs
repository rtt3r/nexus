using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Core.Domain.People.Aggregates;

namespace Nexus.Core.Infra.Data.Configurations.People;

public sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("People");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Type)
            .HasMaxLength(7);

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
        builder.ToTable("NaturalPeople");

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

internal sealed class LegalPersonConfiguration : IEntityTypeConfiguration<LegalPerson>
{
    public void Configure(EntityTypeBuilder<LegalPerson> builder)
    {
        builder.ToTable("LegalPeople");

        builder.ComplexProperty(o => o.Name, name =>
        {
            name.Property(p => p.BrandName)
                .HasColumnName(nameof(LegalPersonName.BrandName))
                .HasMaxLength(128)
                .IsRequired();

            name.Property(p => p.CorporateName)
                .HasColumnName(nameof(LegalPersonName.CorporateName))
                .HasMaxLength(256)
                .IsRequired();
        });
    }
}

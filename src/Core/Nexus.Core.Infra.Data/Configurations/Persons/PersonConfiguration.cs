using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Core.Domain.Persons.Aggregates;

namespace Nexus.Core.Infra.Data.Configurations.Persons;

internal sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
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

internal sealed class PersonDocumentConfiguration : IEntityTypeConfiguration<PersonDocument>
{
    public void Configure(EntityTypeBuilder<PersonDocument> builder)
    {
        builder.ToTable("PersonDocuments", "Core");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.PersonId)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.DocumentId)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Value)
            .HasMaxLength(64)
            .IsRequired();

        builder.HasIndex(p => new { p.PersonId, p.DocumentId, p.Value })
            .IsUnique();

        builder.HasMany(p => p.Attributes)
            .WithOne(p => p.Document)
            .HasForeignKey(p => p.DocumentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
internal sealed class PersonDocumentAttributeConfiguration : IEntityTypeConfiguration<PersonDocumentAttribute>
{
    public void Configure(EntityTypeBuilder<PersonDocumentAttribute> builder)
    {
        builder.ToTable("PersonDocumentAttributes", "Core");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.DocumentId)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.AttributeId)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Value)
            .HasMaxLength(128)
            .IsRequired();
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Core.Domain.People.Aggregates;

namespace Nexus.Core.Infra.Data.Configurations.People;

public sealed class PersonDocumentConfiguration : IEntityTypeConfiguration<PersonDocument>
{
    public void Configure(EntityTypeBuilder<PersonDocument> builder)
    {
        builder.ToTable("PersonDocuments");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.PersonId)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.TypeId)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Number)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Issuer)
            .HasMaxLength(16);

        builder.HasIndex(p => p.Number)
            .IsUnique();

        builder.HasIndex(p => new { p.PersonId, p.TypeId, p.Number })
            .IsUnique();
    }
}

internal sealed class PersonDocumentTypeConfiguration : IEntityTypeConfiguration<PersonDocumentType>
{
    public void Configure(EntityTypeBuilder<PersonDocumentType> builder)
    {
        builder.ToTable("PersonDocumentTypes");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Name)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(256);

        builder.HasMany(p => p.Documents)
            .WithOne(p => p.Type)
            .HasForeignKey(p => p.TypeId);

        builder.HasIndex(p => p.Name)
            .IsUnique();
    }
}

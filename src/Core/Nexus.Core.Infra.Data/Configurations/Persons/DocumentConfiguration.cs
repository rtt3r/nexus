using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Core.Domain.Persons.Aggregates;

namespace Nexus.Core.Infra.Data.Configurations.Persons;

public sealed class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("PersonDocuments", "Core");
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

        builder.HasIndex(p => p.Number)
            .IsUnique();

        builder.HasIndex(p => new { p.PersonId, p.TypeId, p.Number })
            .IsUnique();
    }
}

internal sealed class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
{
    public void Configure(EntityTypeBuilder<DocumentType> builder)
    {
        builder.ToTable("PersonDocumentTypes", "Core");
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

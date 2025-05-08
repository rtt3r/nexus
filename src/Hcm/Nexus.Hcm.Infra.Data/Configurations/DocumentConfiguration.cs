using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Hcm.Domain.People.Aggregates;

namespace Nexus.Hcm.Infra.Data.Configurations;

internal sealed class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("Documents", "Core");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Name)
            .HasMaxLength(64)
            .IsRequired();

        builder.HasMany(p => p.PersonDocuments)
            .WithOne(p => p.Document)
            .HasForeignKey(p => p.DocumentId);

        builder.HasMany(p => p.Attributes)
            .WithOne(p => p.Document)
            .HasForeignKey(p => p.DocumentId);
    }
}

internal sealed class DocumentAttributeConfiguration : IEntityTypeConfiguration<DocumentAttribute>
{
    public void Configure(EntityTypeBuilder<DocumentAttribute> builder)
    {
        builder.ToTable("DocumentAttributes", "Core");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.DocumentId)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Name)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Type)
            .IsRequired();
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Core.Infra.Data.EventStore;

namespace Nexus.Core.Infra.Data.Configurations.EventSourcing;

internal sealed class StoredEventConfiguration : IEntityTypeConfiguration<StoredEvent>
{
    public void Configure(EntityTypeBuilder<StoredEvent> builder)
    {
        builder.ToTable("StoredEvents");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(p => p.AggregateId)
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(c => c.EventType)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(c => c.Data)
            .IsRequired();

        builder.Property(c => c.User)
            .HasMaxLength(64);
    }
}

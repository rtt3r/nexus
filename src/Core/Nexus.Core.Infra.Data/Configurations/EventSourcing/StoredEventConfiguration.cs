using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Core.Infra.Data.EventSourcing;

namespace Nexus.Core.Infra.Data.Configurations.EventSourcing;

internal sealed class StoredEventConfiguration : IEntityTypeConfiguration<StoredEvent>
{
    public void Configure(EntityTypeBuilder<StoredEvent> builder)
    {
        builder.ToTable("StoredEvents");
        builder.HasKey(p => p.Id);

        builder.Property(c => c.EventType)
            .HasMaxLength(100)
            .IsRequired();
    }
}

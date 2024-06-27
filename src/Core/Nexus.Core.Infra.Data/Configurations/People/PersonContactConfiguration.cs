using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Core.Domain.People.Aggregates;

namespace Nexus.Core.Infra.Data.Configurations.People
{
    public sealed class PersonContactConfiguration : IEntityTypeConfiguration<PersonContact>
    {
        public void Configure(EntityTypeBuilder<PersonContact> builder)
        {
            builder.ToTable("PersonContacts");
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

            builder.Property(p => p.Value)
                .HasMaxLength(128)
                .IsRequired();

            builder.HasIndex(p => new { p.PersonId, p.TypeId, p.Value })
                .IsUnique();
        }
    }

    internal sealed class PersonContactTypeConfiguration : IEntityTypeConfiguration<PersonContactType>
    {
        public void Configure(EntityTypeBuilder<PersonContactType> builder)
        {
            builder.ToTable("PersonContactTypes");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(256);

            builder.HasMany(p => p.Contacts)
                .WithOne(p => p.Type)
                .HasForeignKey(p => p.TypeId);

            builder.HasIndex(p => p.Name)
                .IsUnique();
        }
    }
}

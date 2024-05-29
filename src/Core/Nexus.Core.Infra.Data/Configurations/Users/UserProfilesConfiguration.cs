using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Infra.Data.Configurations.Users;

internal sealed class UserProfilesConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("UserProfiles");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Avatar)
            .HasMaxLength(256);

        builder.Property(p => p.Biography)
            .HasMaxLength(1024);

        builder.Property(p => p.Headline)
            .HasMaxLength(128);
    }
}

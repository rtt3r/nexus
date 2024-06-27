using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Core.Domain.People.Aggregates;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Infra.Data.Configurations.Users;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasMaxLength(36)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Avatar)
            .HasMaxLength(256);

        builder.Property(p => p.Username)
            .HasMaxLength(64)
            .IsRequired();

        builder.HasOne(e => e.Person)
            .WithOne(e => e.User)
            .HasForeignKey<NaturalPerson>(e => e.Id);
    }
}
using Microsoft.EntityFrameworkCore;
using Nexus.Core.Domain.Customers.Aggregates;
using Nexus.Core.Domain.People.Aggregates;
using Nexus.Core.Domain.Users.Aggregates;
using Nexus.Core.Infra.Data.Configurations.Customers;
using Nexus.Core.Infra.Data.Configurations.People;
using Nexus.Core.Infra.Data.Configurations.Users;

namespace Nexus.Core.Infra.Data;

public abstract class CoreDbContext(DbContextOptions options)
    : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<PersonAddress> PersonAddresses { get; set; } = null!;
    public DbSet<PersonPhone> PersonPhones { get; set; } = null!;
    public DbSet<PersonEmail> PersonEmails { get; set; } = null!;
    public DbSet<PersonDocument> PersonDocuments { get; set; } = null!;
    public DbSet<PersonDocumentType> PersonDocumentType { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UsersConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        modelBuilder.ApplyConfiguration(new NaturalPersonConfiguration());
        modelBuilder.ApplyConfiguration(new LegalPersonConfiguration());
        modelBuilder.ApplyConfiguration(new PersonAddressConfiguration());
        modelBuilder.ApplyConfiguration(new PersonPhoneConfiguration());
        modelBuilder.ApplyConfiguration(new PersonEmailConfiguration());
        modelBuilder.ApplyConfiguration(new PersonDocumentConfiguration());
        modelBuilder.ApplyConfiguration(new PersonDocumentTypeConfiguration());
    }
}

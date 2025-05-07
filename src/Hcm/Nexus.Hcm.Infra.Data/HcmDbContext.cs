using Microsoft.EntityFrameworkCore;
using Nexus.Hcm.Domain.Persons.Aggregates;
using Nexus.Hcm.Infra.Data.Configurations;

namespace Nexus.Hcm.Infra.Data;

internal sealed class HcmDbContext(DbContextOptions<HcmDbContext> options)
    : DbContext(options)
{
    public DbSet<Person> Persons { get; set; } = default!;
    public DbSet<NaturalPerson> NaturalPersons { get; set; } = default!;
    public DbSet<PersonDocument> PersonDocuments { get; set; } = default!;
    public DbSet<Document> Documents { get; set; } = default!;
    public DbSet<DocumentAttribute> DocumentAttributes { get; set; } = default!;
    public DbSet<PersonDocumentAttribute> PersonDocumentAttributes { get; set; } = default!;
    public DbSet<PersonAddress> PersonAddresses { get; set; } = default!;
    public DbSet<PersonContact> PersonContacts { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DocumentConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentAttributeConfiguration());
        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        modelBuilder.ApplyConfiguration(new NaturalPersonConfiguration());
        modelBuilder.ApplyConfiguration(new PersonDocumentConfiguration());
        modelBuilder.ApplyConfiguration(new PersonDocumentAttributeConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
    }
}

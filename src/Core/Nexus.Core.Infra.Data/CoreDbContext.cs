using Microsoft.EntityFrameworkCore;
using Nexus.Core.Domain.Accounts.Aggregates;
using Nexus.Core.Infra.Data.Configurations.Accounts;

namespace Nexus.Core.Infra.Data;

internal sealed class CoreDbContext(DbContextOptions<CoreDbContext> options)
    : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; } = default!;
    public DbSet<FinancialInstitution> FinancialInstitutions { get; set; } = default!;
    //public DbSet<Transaction> Transactions { get; set; } = default!;
    //public DbSet<TransactionCategory> TransactionCategories { get; set; } = default!;
    //public DbSet<TransactionPaymentMethod> TransactionPaymentMethods { get; set; } = default!;
    //public DbSet<TransactionSubCategory> TransactionSubCategories { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new FinancialInstitutionConfiguration());
    }
}

using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Nexus.Finance.Infra.Data;

internal class FinanceDbContextFactory : DesignTimeDbContextFactory<FinanceDbContext>
{
    protected override FinanceDbContext CreateNewInstance(DbContextOptionsBuilder<FinanceDbContext> optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        return new FinanceDbContext(optionsBuilder.Options);
    }
}

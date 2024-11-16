using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Nexus.Infra.EventSourcing;

internal sealed class EventSourcingDbContextFactory : DesignTimeDbContextFactory<EventSourcingDbContext>
{
    protected override EventSourcingDbContext CreateNewInstance(DbContextOptionsBuilder<EventSourcingDbContext> optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("EventSourcingConnection"));
        return new EventSourcingDbContext(optionsBuilder.Options);
    }
}

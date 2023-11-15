using Nexus.Core.Infra.Data.EventSourcing;
using Microsoft.EntityFrameworkCore;

namespace Nexus.Core.Infra.Data.SqlServer;

public class SqlServerEventSourcingDbContext : EventSourcingDbContext
{
    public SqlServerEventSourcingDbContext(DbContextOptions options)
        : base(options)
    { }
}

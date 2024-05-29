using Microsoft.EntityFrameworkCore;
using Nexus.Core.Infra.Data.EventSourcing;

namespace Nexus.Core.Infra.Data.SqlServer;

public class SqlServerEventSourcingDbContext(DbContextOptions options) : EventSourcingDbContext(options)
{
}

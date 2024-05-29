using Microsoft.EntityFrameworkCore;
using Nexus.Core.Infra.Data.EventSourcing;

namespace Nexus.Core.Infra.Data.MySql;

public class MySqlEventSourcingDbContext(DbContextOptions options) : EventSourcingDbContext(options)
{
}

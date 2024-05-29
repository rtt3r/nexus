using Nexus.Core.Infra.Data.MySql;
using Nexus.Core.Infra.Data.Npgsql;
using Nexus.Core.Infra.Data.SqlServer;
using Nexus.Infra.Crosscutting.Providers.Data;

namespace Nexus.Core.Infra.IoC.Providers;

public static class DbProviderFactory
{
    private static IDbProviderFactory? coreDbProviderFactory = null;
    private static IDbProviderFactory? eventSourcingDbProviderFactory = null;

    public static IDbProviderFactory Core => coreDbProviderFactory ??= new CoreDbProviderFactory();
    public static IDbProviderFactory EventSourcing => eventSourcingDbProviderFactory ??= new EventSourcingDbProviderFactory();

    private class CoreDbProviderFactory : IDbProviderFactory
    {
        public IDbProvider CreateProvider(DbProvider provider)
        {
            return provider switch
            {
                DbProvider.SqlServer => new SqlServerCoreDbProvider(),
                DbProvider.MySql => new MySqlCoreDbProvider(),
                DbProvider.Npgsql => new NpgsqlCoreDbProvider(),
                _ => new SqlServerCoreDbProvider(),
            };
        }
    }

    private class EventSourcingDbProviderFactory : IDbProviderFactory
    {
        public IDbProvider CreateProvider(DbProvider provider)
        {
            return provider switch
            {
                DbProvider.SqlServer => new SqlServerEventSourcingDbProvider(),
                DbProvider.MySql => new MySqlEventSourcingDbProvider(),
                DbProvider.Npgsql => new NpgsqlEventSourcingDbProvider(),
                _ => new SqlServerEventSourcingDbProvider(),
            };
        }
    }
}

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

    class CoreDbProviderFactory : IDbProviderFactory
    {
        public IDbProvider CreateProvider(DbProvider provider)
        {
            switch (provider)
            {
                case DbProvider.SqlServer:
                    return new SqlServerCoreDbProvider();
                case DbProvider.MySql:
                    return new MySqlCoreDbProvider();
                case DbProvider.Npgsql:
                    return new NpgsqlCoreDbProvider();
                default:
                    return new SqlServerCoreDbProvider();
            }
        }
    }

    class EventSourcingDbProviderFactory : IDbProviderFactory
    {
        public IDbProvider CreateProvider(DbProvider provider)
        {
            switch (provider)
            {
                case DbProvider.SqlServer:
                    return new SqlServerEventSourcingDbProvider();
                case DbProvider.MySql:
                    return new MySqlEventSourcingDbProvider();
                case DbProvider.Npgsql:
                    return new NpgsqlEventSourcingDbProvider();
                default:
                    return new SqlServerEventSourcingDbProvider();
            }
        }
    }
}

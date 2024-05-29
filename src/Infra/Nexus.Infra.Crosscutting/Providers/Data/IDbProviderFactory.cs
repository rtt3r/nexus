namespace Nexus.Infra.Crosscutting.Providers.Data;

public interface IDbProviderFactory
{
    IDbProvider CreateProvider(DbProvider provider);
}
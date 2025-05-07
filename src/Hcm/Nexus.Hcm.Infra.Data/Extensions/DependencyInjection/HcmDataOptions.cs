namespace Nexus.Hcm.Infra.Data.Extensions.DependencyInjection;

public sealed class HcmDataOptions
{
    public string ConnectionString { get; private set; } = default!;

    public void UseConnectionString(string connectionString)
        => ConnectionString = connectionString;

}
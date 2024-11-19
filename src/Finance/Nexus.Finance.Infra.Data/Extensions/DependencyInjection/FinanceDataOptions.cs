namespace Nexus.Finance.Infra.Data.Extensions.DependencyInjection;

public sealed class FinanceDataOptions
{
    public string ConnectionString { get; private set; } = default!;

    public void UseConnectionString(string connectionString)
        => ConnectionString = connectionString;

}
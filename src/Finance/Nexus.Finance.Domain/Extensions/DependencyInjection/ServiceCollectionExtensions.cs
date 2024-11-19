using Microsoft.Extensions.DependencyInjection;

namespace Nexus.Finance.Domain.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFinanceDomain(this IServiceCollection services, Action<FinanceDomainOptions> action)
    {
        var options = new FinanceDomainOptions();

        action?.Invoke(options);

        return services;
    }
}

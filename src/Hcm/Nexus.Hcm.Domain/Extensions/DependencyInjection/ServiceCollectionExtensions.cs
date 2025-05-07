using Microsoft.Extensions.DependencyInjection;

namespace Nexus.Hcm.Domain.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHcmDomain(this IServiceCollection services, Action<HcmDomainOptions>? action = null)
    {
        var options = new HcmDomainOptions();

        action?.Invoke(options);

        return services;
    }
}

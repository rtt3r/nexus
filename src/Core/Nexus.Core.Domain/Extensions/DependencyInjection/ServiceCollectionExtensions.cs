using Microsoft.Extensions.DependencyInjection;

namespace Nexus.Core.Domain.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreDomain(this IServiceCollection services, Action<CoreDomainOptions> action)
    {
        var options = new CoreDomainOptions();

        action?.Invoke(options);

        return services;
    }
}

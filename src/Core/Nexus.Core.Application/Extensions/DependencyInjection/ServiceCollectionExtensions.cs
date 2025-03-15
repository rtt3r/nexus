using Microsoft.Extensions.DependencyInjection;

namespace Nexus.Core.Application.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreApplication(this IServiceCollection services, Action<CoreApplicationOptions>? action = null)
    {
        var options = new CoreApplicationOptions();

        action?.Invoke(options);

        if (options.MediatRAssemblies.Length != 0)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(options.MediatRAssemblies);
            });
        }

        return services;
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace Nexus.Hcm.Application.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHcmApplication(this IServiceCollection services, Action<HcmApplicationOptions>? action = null)
    {
        var options = new HcmApplicationOptions();

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

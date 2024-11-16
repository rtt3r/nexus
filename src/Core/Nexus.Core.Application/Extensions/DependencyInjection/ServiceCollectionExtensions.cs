using Goal.Infra.Crosscutting.Adapters;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Core.Application.TypeAdapters;

namespace Nexus.Core.Application.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreApplication(this IServiceCollection services, Action<CoreApplicationOptions>? action = null)
    {
        var options = new CoreApplicationOptions();

        action?.Invoke(options);

        services.AddAutoMapperTypeAdapter();

        if (options.MediatRAssemblies.Length != 0)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(options.MediatRAssemblies);
            });
        }

        return services;
    }

    public static IServiceCollection AddAutoMapperTypeAdapter(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperAdapterFactory).Assembly);
        services.AddSingleton<ITypeAdapterFactory, AutoMapperAdapterFactory>();
        services.AddSingleton(factory => factory.GetService<ITypeAdapterFactory>()!.Create());

        return services;
    }
}

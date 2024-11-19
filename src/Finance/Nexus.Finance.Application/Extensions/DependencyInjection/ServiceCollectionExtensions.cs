using Goal.Infra.Crosscutting.Adapters;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Finance.Application.TypeAdapters;

namespace Nexus.Finance.Application.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFinanceApplication(this IServiceCollection services, Action<FinanceApplicationOptions>? action = null)
    {
        var options = new FinanceApplicationOptions();

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

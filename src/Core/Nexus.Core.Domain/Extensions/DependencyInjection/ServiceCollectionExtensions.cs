using Microsoft.Extensions.DependencyInjection;
using Nexus.Core.Domain.Users.Services;
using Nexus.Infra.Crosscutting.Settings;

namespace Nexus.Core.Domain.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreDomain(this IServiceCollection services, Action<CoreDomainOptions> action)
    {
        var options = new CoreDomainOptions();

        services.Configure((UiAvatarsOptions opts) =>
        {
            opts.BaseAddress = options.UiAvatar.BaseAddress;
            opts.DefaultBackground = options.UiAvatar.DefaultBackground;
            opts.DefaultColor = options.UiAvatar.DefaultColor;
        });

        services.AddScoped<IGenerateUserAvatarDomainService, GenerateUserAvatarDomainService>();

        return services;
    }
}

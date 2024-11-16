using Nexus.Infra.Crosscutting.Settings;

namespace Nexus.Core.Domain.DependencyInjection;

public sealed class CoreDomainOptions
{
    public UiAvatarsOptions UiAvatar { get; set; } = new UiAvatarsOptions();
}
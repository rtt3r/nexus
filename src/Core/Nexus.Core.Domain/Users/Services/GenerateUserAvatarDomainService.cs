using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Nexus.Core.Domain.Users.Aggregates;
using Nexus.Infra.Crosscutting.Settings;

namespace Nexus.Core.Domain.Users.Services;

internal sealed class GenerateUserAvatarDomainService(IOptions<UiAvatarsOptions> uiAvatarsOptions)
    : IGenerateUserAvatarDomainService
{
    private readonly UiAvatarsOptions uiAvatarsOptions = uiAvatarsOptions.Value;

    public void GenerateTemporaryAvatar(User user)
        => GenerateTemporaryAvatar(user, uiAvatarsOptions.DefaultBackground);

    public void GenerateTemporaryAvatar(User user, string background)
        => GenerateTemporaryAvatar(user, background, uiAvatarsOptions.DefaultColor);

    public void GenerateTemporaryAvatar(User user, string background, string color)
    {
        var query = new Dictionary<string, string?>()
        {
            { "background", background },
            { "color", color },
            { "name", user.Username }
        };

        user.UpdateAvatar(
            QueryHelpers.AddQueryString(uiAvatarsOptions.BaseAddress, query)
        );
    }
}
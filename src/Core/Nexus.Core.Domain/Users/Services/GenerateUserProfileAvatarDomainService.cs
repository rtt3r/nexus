using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Nexus.Core.Domain.Users.Aggregates;
using Nexus.Infra.Crosscutting.Settings;

namespace Nexus.Core.Domain.Users.Services;

public class GenerateUserProfileAvatarDomainService(IOptions<UiAvatarsOptions> uiAvatarsOptions)
    : IGenerateUserProfileAvatarDomainService
{
    private readonly UiAvatarsOptions uiAvatarsOptions = uiAvatarsOptions.Value;

    public void GenerateTemporaryAvatar(UserAccount account)
        => GenerateTemporaryAvatar(account, uiAvatarsOptions.DefaultBackground);

    public void GenerateTemporaryAvatar(UserAccount account, string background)
        => GenerateTemporaryAvatar(account, background, uiAvatarsOptions.DefaultColor);

    public void GenerateTemporaryAvatar(UserAccount account, string background, string color)
    {
        var query = new Dictionary<string, string?>()
        {
            { "background", background },
            { "color", color },
            { "name", account.Name }
        };

        account.Profile.UpdateAvatar(
            QueryHelpers.AddQueryString(uiAvatarsOptions.BaseAddress, query)
        );
    }
}
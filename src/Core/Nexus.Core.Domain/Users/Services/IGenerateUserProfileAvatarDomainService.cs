using Goal.Domain.Services;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Domain.Users.Services;

public interface IGenerateUserProfileAvatarDomainService : IDomainService
{
    void GenerateTemporaryAvatar(UserAccount account);
    void GenerateTemporaryAvatar(UserAccount account, string background);
    void GenerateTemporaryAvatar(UserAccount account, string background, string color);
}
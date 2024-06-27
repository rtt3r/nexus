using Goal.Domain.Services;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Domain.Users.Services;

public interface IGenerateUserAvatarDomainService : IDomainService
{
    void GenerateTemporaryAvatar(User user);
    void GenerateTemporaryAvatar(User user, string background);
    void GenerateTemporaryAvatar(User user, string background, string color);
}
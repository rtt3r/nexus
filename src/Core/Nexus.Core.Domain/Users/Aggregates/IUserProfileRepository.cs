using Goal.Seedwork.Domain.Aggregates;

namespace Nexus.Core.Domain.Users.Aggregates;

public interface IUserProfileRepository : IRepository<UserProfile, string>
{
}

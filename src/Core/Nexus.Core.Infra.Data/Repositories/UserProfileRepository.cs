using Goal.Seedwork.Infra.Data;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Infra.Data.Repositories;

public class UserProfileRepository : Repository<UserProfile, string>, IUserProfileRepository
{
    public UserProfileRepository(CoreDbContext context)
        : base(context)
    {
    }
}

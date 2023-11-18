using Nexus.Core.Model.Users;
using Nexus.Infra.Data.Query;
using Raven.Client.Documents.Session;

namespace Nexus.Core.Infra.Data.Query.Repositories.Users
{
    public class UserProfileQueryRepository : RavenQueryRepository<UserProfile>, IUserProfileQueryRepository
    {
        public UserProfileQueryRepository(IAsyncDocumentSession dbSession)
            : base(dbSession)
        {
        }
    }
}
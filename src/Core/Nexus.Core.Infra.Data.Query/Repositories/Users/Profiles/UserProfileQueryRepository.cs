using Nexus.Core.Model.Users;
using Nexus.Infra.Data.Query;
using Raven.Client.Documents.Session;

namespace Nexus.Core.Infra.Data.Query.Repositories.Users.Profiles;

public class UserProfileQueryRepository(IAsyncDocumentSession dbSession) : RavenQueryRepository<UserProfile>(dbSession), IUserProfileQueryRepository
{
}
using Nexus.Core.Model.Users;
using Nexus.Infra.Data.Query;
using Raven.Client.Documents.Session;

namespace Nexus.Core.Infra.Data.Query.Repositories.Users;

public class UserQueryRepository : RavenQueryRepository<User>, IUserQueryRepository
{
    public UserQueryRepository(IAsyncDocumentSession dbSession)
        : base(dbSession)
    {
    }
}

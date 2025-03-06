using Nexus.Core.Model.BusinessGroups;
using Nexus.Infra.Data.Query;
using Raven.Client.Documents.Session;

namespace Nexus.Core.Infra.Data.Query.Repositories.BusinessGroups;

internal class BusinessGroupQueryRepository(IAsyncDocumentSession dbSession)
    : RavenQueryRepository<BusinessGroup>(dbSession), IBusinessGroupQueryRepository
{
}

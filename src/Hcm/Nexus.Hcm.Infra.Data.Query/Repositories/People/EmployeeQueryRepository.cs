using Nexus.Hcm.Model.People;
using Nexus.Infra.Data.Query;
using Raven.Client.Documents.Session;

namespace Nexus.Hcm.Infra.Data.Query.Repositories.People;

internal class EmployeeQueryRepository(IAsyncDocumentSession dbSession)
    : RavenQueryRepository<Employee>(dbSession), IEmployeeQueryRepository
{
}

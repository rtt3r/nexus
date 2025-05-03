using Nexus.Core.Model.Companies;
using Nexus.Infra.Data.Query;
using Raven.Client.Documents.Session;

namespace Nexus.Core.Infra.Data.Query.Repositories.Companies;

internal class CompanyQueryRepository(IAsyncDocumentSession dbSession)
    : RavenQueryRepository<Company>(dbSession), ICompanyQueryRepository
{
}

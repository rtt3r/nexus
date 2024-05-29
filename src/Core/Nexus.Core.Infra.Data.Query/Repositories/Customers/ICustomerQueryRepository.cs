using Goal.Infra.Data.Query;
using Nexus.Core.Model.Customers;

namespace Nexus.Core.Infra.Data.Query.Repositories.Customers;

public interface ICustomerQueryRepository : IQueryRepository<Customer, string>
{
}

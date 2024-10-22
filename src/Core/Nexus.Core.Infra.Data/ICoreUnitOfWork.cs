using Goal.Domain;
using Nexus.Core.Domain.Customers.Aggregates;
using Nexus.Core.Domain.People.Aggregates;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Infra.Data;

public interface ICoreUnitOfWork : IUnitOfWork
{
    ICustomerRepository Customers { get; }
    IUserRepository Users { get; }
    IPersonRepository People { get; }
}

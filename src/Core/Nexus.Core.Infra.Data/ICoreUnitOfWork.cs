using Goal.Domain;
using Nexus.Core.Domain.Customers.Aggregates;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Infra.Data;

public interface ICoreUnitOfWork : IUnitOfWork
{
    ICustomerRepository Customers { get; }
    IUserAccountRepository UserAccounts { get; }
    IUserProfileRepository UserProfiles { get; }
}

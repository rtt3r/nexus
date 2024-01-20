using Nexus.Core.Domain.Customers.Aggregates;
using Goal.Infra.Data;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Infra.Data;

public sealed class CoreUnitOfWork(
    CoreDbContext context,
    ICustomerRepository customerRepository,
    IUserAccountRepository userAccountRepository,
    IUserProfileRepository userProfileRepository) : UnitOfWork(context), ICoreUnitOfWork
{
    public ICustomerRepository Customers { get; } = customerRepository;
    public IUserAccountRepository UserAccounts { get; set; } = userAccountRepository;
    public IUserProfileRepository UserProfiles { get; set; } = userProfileRepository;
}

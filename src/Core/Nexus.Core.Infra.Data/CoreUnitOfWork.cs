using Goal.Infra.Data;
using Nexus.Core.Domain.Customers.Aggregates;
using Nexus.Core.Domain.People.Aggregates;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Infra.Data;

public sealed class CoreUnitOfWork(
    CoreDbContext context,
    ICustomerRepository customerRepository,
    IUserRepository userRepository,
    IPersonRepository peopleRepository)
    : UnitOfWork(context), ICoreUnitOfWork
{
    public ICustomerRepository Customers { get; } = customerRepository;
    public IUserRepository Users { get; } = userRepository;
    public IPersonRepository People { get; } = peopleRepository;
}

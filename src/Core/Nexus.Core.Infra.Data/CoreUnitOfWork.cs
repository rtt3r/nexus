using Nexus.Core.Domain.Customers.Aggregates;
using Goal.Seedwork.Infra.Data;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Infra.Data;

public sealed class CoreUnitOfWork : UnitOfWork, ICoreUnitOfWork
{
    public CoreUnitOfWork(
        CoreDbContext context,
        ICustomerRepository customerRepository,
        IUsersRepository usersRepository)
        : base(context)
    {
        Customers = customerRepository;
        Users = usersRepository;
    }

    public ICustomerRepository Customers { get; }
    public IUsersRepository Users { get; set; }
}

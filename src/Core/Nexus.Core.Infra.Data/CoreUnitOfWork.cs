using Nexus.Core.Domain.Customers.Aggregates;
using Goal.Seedwork.Infra.Data;
using Nexus.Core.Domain.Users.Aggregates;

namespace Nexus.Core.Infra.Data;

public sealed class CoreUnitOfWork : UnitOfWork, ICoreUnitOfWork
{
    public CoreUnitOfWork(
        CoreDbContext context,
        ICustomerRepository customerRepository,
        IUserProfileRepository userProfileRepository)
        : base(context)
    {
        Customers = customerRepository;
        UserProfiles = userProfileRepository;
    }

    public ICustomerRepository Customers { get; }
    public IUserProfileRepository UserProfiles { get; set; }
}

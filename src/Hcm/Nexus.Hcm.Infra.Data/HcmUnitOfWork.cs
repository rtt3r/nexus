using Goal.Infra.Data;

namespace Nexus.Hcm.Infra.Data;

internal sealed class HcmUnitOfWork(HcmDbContext context)
    : UnitOfWork(context), IHcmUnitOfWork
{
}

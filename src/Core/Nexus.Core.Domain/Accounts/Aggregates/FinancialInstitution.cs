using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Accounts.Aggregates;

public class FinancialInstitution : Entity
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public string Icon { get; private set; } = default!;
    public IList<Account> Accounts { get; private set; } = [];

    protected FinancialInstitution()
        : base()
    {
    }
}

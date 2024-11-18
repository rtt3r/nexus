using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Finance;

public class Account : Entity
{
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public AccountType Type { get; private set; }
    public string Icon { get; private set; } = null!;
    public decimal InitialBalance { get; private set; }
    public decimal Overdraft { get; private set; }

    protected Account()
        : base()
    {
    }
}

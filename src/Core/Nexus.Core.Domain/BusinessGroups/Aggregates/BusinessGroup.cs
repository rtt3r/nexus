using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.BusinessGroups.Aggregates;

public class BusinessGroup : Entity
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public string? TaxId { get; private set; }
    public bool Active { get; private set; } = true;
    public IList<Company> Companies { get; private set; } = [];

    protected BusinessGroup()
        : base()
    {
    }

    protected BusinessGroup(string name)
        : this()
    {
        SetName(name);
    }

    public void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        Name = name;
    }

    public void SetDescription(string description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(description, nameof(description));
        Description = description;
    }

    public void Activate()
        => Active = true;

    public void Inactivate()
        => Active = false;
}

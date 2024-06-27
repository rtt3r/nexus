using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Shared;

public abstract class EntityTypeDescriptor : Entity
{
    protected EntityTypeDescriptor()
        : base()
    {
    }

    protected EntityTypeDescriptor(string name)
        : this()
    {
        Name = name;
    }

    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }

    public void SetDescription(string? description)
        => Description = description?.Trim();
}
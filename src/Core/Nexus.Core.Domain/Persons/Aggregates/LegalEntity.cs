namespace Nexus.Core.Domain.Persons.Aggregates;

public class LegalEntity : Person
{
    protected LegalEntity()
        : base(PersonType.Legal)
    {
    }

    protected LegalEntity(LegalEntityName name)
        : this()
    {
        Name = name;
    }

    public LegalEntityName Name { get; protected set; } = default!;
    public DateOnly? OpenedAt { get; protected set; }
}

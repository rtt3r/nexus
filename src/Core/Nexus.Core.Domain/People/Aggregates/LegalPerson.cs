namespace Nexus.Core.Domain.People.Aggregates;

public class LegalPerson : Person
{
    protected LegalPerson()
        : base(PersonType.Legal)
    {
    }

    protected LegalPerson(LegalPersonName name)
        : this()
    {
        Name = name;
    }

    public LegalPersonName Name { get; protected set; } = null!;
    public DateOnly? OpenedAt { get; protected set; }
}

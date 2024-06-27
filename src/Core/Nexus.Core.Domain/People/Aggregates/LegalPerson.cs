namespace Nexus.Core.Domain.People.Aggregates;

public class LegalPerson : Person
{
    protected LegalPerson()
        : base(PersonType.Legal)
    {
    }

    protected LegalPerson(LegalPersonName name, DateOnly openedAt)
        : this()
    {
        Name = name;
        OpenedAt = openedAt;
    }

    public LegalPersonName Name { get; private set; } = null!;
    public DateOnly OpenedAt { get; private set; }
}

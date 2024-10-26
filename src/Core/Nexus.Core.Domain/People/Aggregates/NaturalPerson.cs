namespace Nexus.Core.Domain.People.Aggregates;

public class NaturalPerson : Person
{
    protected NaturalPerson()
        : base(PersonType.Natural)
    {
    }

    protected NaturalPerson(NaturalPersonName name)
        : this()
    {
        Name = name;
    }

    public NaturalPersonName Name { get; protected set; } = null!;
    public PersonGender? Gender { get; protected set; }
    public DateOnly? Birthdate { get; protected set; }
}

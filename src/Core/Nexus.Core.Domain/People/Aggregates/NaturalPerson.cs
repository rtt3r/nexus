namespace Nexus.Core.Domain.People.Aggregates;

public class NaturalPerson : Person
{
    protected NaturalPerson()
        : base(PersonType.Natural)
    {
    }

    protected NaturalPerson(NaturalPersonName name, DateOnly birthdate, PersonGender gender)
        : this()
    {
        Name = name;
        Birthdate = birthdate;
        Gender = gender;
    }

    public NaturalPersonName Name { get; private set; } = null!;
    public PersonGender Gender { get; private set; }
    public DateOnly Birthdate { get; private set; }
}

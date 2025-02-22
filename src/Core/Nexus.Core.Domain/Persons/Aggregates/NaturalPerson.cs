namespace Nexus.Core.Domain.Persons.Aggregates;

public class NaturalPerson : Person
{
    protected NaturalPerson()
        : base(PersonType.Natural)
    {
    }

    public NaturalPerson(string firstName, string lastName)
        : this()
    {
        Name = new NaturalPersonName(firstName, lastName);
    }

    public NaturalPersonName Name { get; protected set; } = default!;
    public Gender? Gender { get; protected set; }
    public DateOnly? Birthdate { get; protected set; }

    public void SetGender(string gender)
        => Gender = Enum.Parse<Gender>(gender);

    public void SetBirthdate(DateOnly? birthdate)
        => Birthdate = birthdate;
}

namespace Nexus.Hcm.Domain.People.Aggregates;

public class NaturalPerson : Person
{
    protected NaturalPerson()
        : base()
    {
    }

    public NaturalPerson(string name)
        : base(PersonType.Natural, name)
    {
    }

    public Gender? Gender { get; protected set; }
    public DateOnly? DateOfBirth { get; protected set; }

    public virtual void SetGender(Gender gender)
        => Gender = gender;

    public virtual void SetBirthdate(DateOnly birthdate)
        => DateOfBirth = birthdate;
}

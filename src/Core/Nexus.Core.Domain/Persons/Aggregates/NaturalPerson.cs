namespace Nexus.Core.Domain.Persons.Aggregates;

public class NaturalPerson : Person
{
    protected NaturalPerson()
        : base()
    {
    }

    public NaturalPerson(string name, string cpf)
        : base(PersonType.Natural, name)
    {
        AddDocument(DocumentType.Cpf, cpf);
    }

    public Gender? Gender { get; protected set; }
    public DateOnly? DateOfBirth { get; protected set; }

    public virtual void SetGender(Gender gender)
        => Gender = gender;

    public virtual void SetBirthdate(DateOnly birthdate)
        => DateOfBirth = birthdate;
}

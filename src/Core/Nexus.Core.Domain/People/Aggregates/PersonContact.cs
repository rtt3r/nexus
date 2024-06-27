using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.People.Aggregates;

public class PersonContact : Entity
{
    protected PersonContact()
        : base()
    {
    }

    private PersonContact(PersonContactType type, string value)
        : this()
    {
        Type = type;
        TypeId = type.Id;
        Value = value;
    }

    public string PersonId { get; private set; } = null!;
    public string TypeId { get; private set; } = null!;
    public string Value { get; private set; } = null!;
    public Person Person { get; private set; } = null!;
    public PersonContactType Type { get; private set; } = null!;

    public static PersonContact CreateContact(PersonContactType type, string value)
        => new(type, value);
}

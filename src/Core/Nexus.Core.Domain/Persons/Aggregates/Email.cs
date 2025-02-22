using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Persons.Aggregates;

public class Email : Entity
{
    protected Email()
        : base()
    {
    }

    public Email(string address)
        : this()
    {
        Address = address;
    }

    public string PersonId { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public Person Person { get; private set; } = default!;
}
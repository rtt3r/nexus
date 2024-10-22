using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.People.Aggregates;

public class PersonPhone : Entity
{
    protected PersonPhone()
        : base()
    {
    }

    public PersonPhone(PersonPhoneType type, string countryCode, string number)
        : this(type, number)
    {
        CountryCode = countryCode;
    }

    public PersonPhone(PersonPhoneType type, string number)
        : this()
    {
        Number = number;
    }

    public string PersonId { get; private set; } = null!;
    public string? CountryCode { get; private set; }
    public string Number { get; private set; } = null!;
    public bool Principal { get; private set; }
    public Person Person { get; private set; } = null!;

    public void UpdatePrincipal(bool principal)
        => Principal = principal;
}

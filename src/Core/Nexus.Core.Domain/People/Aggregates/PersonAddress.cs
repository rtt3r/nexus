using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.People.Aggregates;

public class PersonAddress : Entity
{
    protected PersonAddress()
    {
    }

    public PersonAddress(PersonAddressType type, string postalCode, string street, string neighborhood, string city, string state, string country)
        : this()
    {
        Type = type;
        PostalCode = postalCode;
        Street = street;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
    }

    public string PersonId { get; private set; } = null!;
    public PersonAddressType Type { get; private set; }
    public string PostalCode { get; private set; } = null!;
    public string Street { get; private set; } = null!;
    public string? Number { get; private set; }
    public string? Complement { get; private set; }
    public string Neighborhood { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string State { get; private set; } = null!;
    public string Country { get; private set; } = null!;
    public bool Principal { get; private set; }
    public Person Person { get; private set; } = null!;

    public void UpdatePrincipal(bool principal)
        => Principal = principal;
}

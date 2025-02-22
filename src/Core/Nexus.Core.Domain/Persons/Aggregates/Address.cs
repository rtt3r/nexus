using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Persons.Aggregates;

public class Address : Entity
{
    protected Address()
    {
    }

    public Address(string type, string zipCode, string street, string number, string neighborhood, string city, string state, string country)
        : this()
    {
        Type = Enum.Parse<AddressType>(type);
        ZipCode = zipCode;
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
    }

    public string PersonId { get; private set; } = default!;
    public AddressType Type { get; private set; }
    public string ZipCode { get; private set; } = default!;
    public string Street { get; private set; } = default!;
    public string Number { get; private set; } = default!;
    public string? Complement { get; private set; }
    public string Neighborhood { get; private set; } = default!;
    public string City { get; private set; } = default!;
    public string State { get; private set; } = default!;
    public string Country { get; private set; } = default!;
    public Person Person { get; private set; } = default!;

    public void SetComplement(string complement)
        => Complement = complement;
}

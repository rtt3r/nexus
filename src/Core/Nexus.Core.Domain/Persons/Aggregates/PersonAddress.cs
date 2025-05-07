using Goal.Domain.Aggregates;
using Nexus.Core.Domain.Persons.Aggregates;

namespace Nexus.Core.Domain.Persons.Aggregates;

public sealed class PersonAddress : Entity
{
    private PersonAddress()
    {
    }

    public PersonAddress(AddressType type, string zipCode, string street, string number, string neighborhood, string city, string state, string country)
        : this()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(zipCode, nameof(zipCode));
        ArgumentException.ThrowIfNullOrWhiteSpace(street, nameof(street));
        ArgumentException.ThrowIfNullOrWhiteSpace(number, nameof(number));
        ArgumentException.ThrowIfNullOrWhiteSpace(neighborhood, nameof(neighborhood));
        ArgumentException.ThrowIfNullOrWhiteSpace(city, nameof(city));
        ArgumentException.ThrowIfNullOrWhiteSpace(state, nameof(state));
        ArgumentException.ThrowIfNullOrWhiteSpace(country, nameof(country));

        Type = type;
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
    public bool Active { get; private set; } = true;
    public Person Person { get; private set; } = default!;

    public void SetComplement(string complement)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(complement, nameof(complement));
        Complement = complement;
    }

    public void Activate()
        => Active = true;

    public void Inactivate()
        => Active = false;
}

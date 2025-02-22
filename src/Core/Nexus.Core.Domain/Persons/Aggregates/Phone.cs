using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Persons.Aggregates;

public class Phone : Entity
{
    protected Phone()
        : base()
    {
    }

    public Phone(string countryCode, string number)
        : this()
    {
        CountryCode = countryCode;
        Number = number;
    }

    public string PersonId { get; private set; } = default!;
    public string CountryCode { get; private set; } = default!;
    public string Number { get; private set; } = default!;
    public Person Person { get; private set; } = default!;
}

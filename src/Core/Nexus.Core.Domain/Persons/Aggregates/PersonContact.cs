using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Persons.Aggregates;

public sealed class PersonContact : Entity
{
    private PersonContact()
        : base()
    {
    }

    public PersonContact(ContactType type, string name, string email, string landlinePhone, string mobilePhone, string whatsapp)
        : this()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));
        ArgumentException.ThrowIfNullOrWhiteSpace(landlinePhone, nameof(landlinePhone));
        ArgumentException.ThrowIfNullOrWhiteSpace(mobilePhone, nameof(mobilePhone));
        ArgumentException.ThrowIfNullOrWhiteSpace(whatsapp, nameof(whatsapp));

        Type = type;
        Name = name;
        Email = email;
        LandlinePhone = landlinePhone;
        MobilePhone = mobilePhone;
        Whatsapp = whatsapp;
    }

    public string PersonId { get; private set; } = default!;
    public ContactType Type { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string LandlinePhone { get; private set; } = default!;
    public string MobilePhone { get; private set; } = default!;
    public string Whatsapp { get; private set; } = default!;
    public bool Active { get; private set; } = true;
    public Person Person { get; private set; } = default!;

    public void Activate()
        => Active = true;

    public void Inactivate()
        => Active = false;
}

using Goal.Domain.Aggregates;

namespace Nexus.Hcm.Domain.People.Aggregates;

public class Person : Entity
{
    public PersonType PersonType { get; protected set; } = default!;
    public string Name { get; protected set; } = default!;
    public bool Active { get; protected set; } = true;
    public IEnumerable<PersonDocument> Documents { get; protected set; } = [];
    public IEnumerable<PersonContact> Contacts { get; protected set; } = [];
    public IEnumerable<PersonAddress> Addresses { get; protected set; } = [];

    protected Person()
        : base()
    {
    }

    protected Person(PersonType type, string name)
    {
        PersonType = type;
        SetName(name);
    }

    public virtual PersonDocument AddDocument(Document document, string value)
    {
        var personDocument = new PersonDocument(this, document, value);

        Documents = [.. Documents, personDocument];

        return personDocument;
    }

    public virtual void RemoveDocument(PersonDocument document)
    {
        ArgumentNullException.ThrowIfNull(document, nameof(document));
        Documents = [.. Documents.Where(d => d.Id != document.Id)];
    }

    public virtual PersonContact AddContact(ContactType type, string name, string email, string landlinePhone, string mobilePhone, string whatsapp)
    {
        var phone = new PersonContact(type, name, email, landlinePhone, mobilePhone, whatsapp);

        Contacts = [.. Contacts, phone];

        return phone;
    }

    public virtual void RemovePhone(PersonContact contact)
    {
        ArgumentNullException.ThrowIfNull(contact, nameof(contact));
        Contacts = [.. Contacts.Where(c => c.Id != contact.Id)];
    }

    public virtual PersonAddress AddAddress(AddressType type, string zipCode, string street, string number, string neighborhood, string city, string state, string country)
    {
        var address = new PersonAddress(type, zipCode, street, number, neighborhood, city, state, country);

        Addresses = [.. Addresses, address];

        return address;
    }

    public virtual void RemoveAddress(PersonAddress address)
    {
        ArgumentNullException.ThrowIfNull(address, nameof(address));
        Addresses = [.. Addresses.Where(c => c.Id != address.Id)];
    }

    public void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        Name = name;
    }

    public void Activate()
        => Active = true;

    public void Inactivate()
        => Active = false;
}

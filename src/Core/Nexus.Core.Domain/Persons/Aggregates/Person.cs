using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Persons.Aggregates;

public class Person : Entity
{
    public PersonType PersonType { get; protected set; } = default!;
    public string Name { get; protected set; } = default!;
    public bool Active { get; protected set; } = true;
    public IList<Document> Documents { get; protected set; } = [];
    public IList<Contact> Contacts { get; protected set; } = [];
    public IList<Address> Addresses { get; protected set; } = [];

    protected Person()
        : base()
    {
    }

    protected Person(PersonType type, string name)
    {
        PersonType = type;
        SetName(name);
    }

    public virtual Document AddDocument(DocumentType type, string number)
    {
        var document = new Document(type, number);

        Documents = [.. Documents, document];

        return document;
    }

    public virtual void RemoveDocument(Document document)
    {
        ArgumentNullException.ThrowIfNull(document, nameof(document));
        Documents = [.. Documents.Where(d => d.Id != document.Id)];
    }

    public virtual Contact AddContact(ContactType type, string name, string email, string landlinePhone, string mobilePhone, string whatsapp)
    {
        var phone = new Contact(type, name, email, landlinePhone, mobilePhone, whatsapp);

        Contacts = [.. Contacts, phone];

        return phone;
    }

    public virtual void RemovePhone(Contact contact)
    {
        ArgumentNullException.ThrowIfNull(contact, nameof(contact));
        Contacts = [.. Contacts.Where(c => c.Id != contact.Id)];
    }

    public virtual Address AddAddress(AddressType type, string zipCode, string street, string number, string neighborhood, string city, string state, string country)
    {
        var address = new Address(type, zipCode, street, number, neighborhood, city, state, country);

        Addresses = [.. Addresses, address];

        return address;
    }

    public virtual void RemoveAddress(Address address)
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

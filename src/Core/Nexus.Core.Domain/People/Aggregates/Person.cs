using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.People.Aggregates;

public abstract class Person(PersonType type) : Entity
{
    public PersonType Type { get; private set; } = type;
    public IEnumerable<PersonDocument> Documents { get; private set; } = [];
    public IEnumerable<PersonPhone> Contacts { get; private set; } = [];
    public IEnumerable<PersonAddress> Addresses { get; private set; } = [];

    protected PersonDocument AddDocument(PersonDocumentType type, string number)
    {
        var document = PersonDocument.CreateDocument(type, number);

        Documents = Documents
            .Append(document)
            .ToList();

        return document;
    }

    protected PersonPhone AddPhone(PersonPhoneType type, string countryCode, string number)
    {
        var contact = new PersonPhone(type, countryCode, number);

        Contacts = Contacts
            .Append(contact)
            .ToList();

        return contact;
    }

    public void RemoveDocument(PersonDocument document)
    {
        Documents = Documents
            .Where(d => d.Id != document.Id)
            .ToList();
    }

    public void RemovePhone(PersonPhone contact)
    {
        Contacts = Contacts
            .Where(c => c.Id != contact.Id)
            .ToList();
    }

    public PersonAddress AddAddress(PersonAddressType type, string postalCode, string street, string neighborhood, string city, string state, string country)
    {
        var address = new PersonAddress(type, postalCode, street, neighborhood, city, state, country);

        Addresses = Addresses
            .Append(address)
            .ToList();

        return address;
    }

    public void RemoveAddress(PersonAddress address)
    {
        Addresses = Addresses
            .Where(a => a.Id != address.Id)
            .ToList();
    }
}

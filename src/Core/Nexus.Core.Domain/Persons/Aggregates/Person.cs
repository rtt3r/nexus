using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Persons.Aggregates;

public abstract class Person(PersonType type) : Entity
{
    public PersonType Type { get; private set; } = type;
    public IList<Document> Documents { get; private set; } = [];
    public IList<Phone> Phones { get; private set; } = [];
    public IList<Email> Emails { get; private set; } = [];
    public IList<Address> Addresses { get; private set; } = [];

    public Document AddDocument(DocumentType type, string number)
    {
        var document = Document.CreateDocument(type, number);

        Documents = [.. Documents, document];

        return document;
    }

    public void RemoveDocument(Document document)
        => Documents = [.. Documents.Where(d => d.Id != document.Id)];

    public Phone AddPhone(string countryCode, string number)
    {
        var phone = new Phone(countryCode, number);

        Phones = [.. Phones, phone];

        return phone;
    }

    public void RemovePhone(Phone contact)
        => Phones = [.. Phones.Where(c => c.Id != contact.Id)];

    public Address AddAddress(string type, string zipCode, string street, string number, string neighborhood, string city, string state, string country)
    {
        var address = new Address(type, zipCode, street, number, neighborhood, city, state, country);

        Addresses = [.. Addresses, address];

        return address;
    }

    public void RemoveAddress(Address address)
        => Addresses = [.. Addresses.Where(a => a.Id != address.Id)];

    public Email AddEmail(string mailAddress)
        => AddEmail(mailAddress, false);

    public Email AddEmail(string mailAddress, bool principal)
    {
        var email = new Email(mailAddress);

        Emails = [.. Emails, email];

        return email;
    }

    public void RemoveEmail(Email email)
        => Emails = [.. Emails.Where(c => c.Id != email.Id)];
}

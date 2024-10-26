using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.People.Aggregates;

public abstract class Person(PersonType type) : Entity
{
    public PersonType Type { get; private set; } = type;
    public IList<PersonDocument> Documents { get; private set; } = [];
    public IList<PersonPhone> Phones { get; private set; } = [];
    public IList<PersonEmail> Emails { get; private set; } = [];
    public IList<PersonAddress> Addresses { get; private set; } = [];

    public PersonEmail GetPrincipalEmail()
        => Emails.First(p => p.Principal);

    protected PersonDocument AddDocument(PersonDocumentType type, string number)
    {
        var document = PersonDocument.CreateDocument(type, number);

        Documents = [.. Documents, document];

        return document;
    }

    public void RemoveDocument(PersonDocument document)
    {
        Documents = Documents
            .Where(d => d.Id != document.Id)
            .ToList();
    }

    protected PersonPhone AddPhone(PersonPhoneType type, string countryCode, string number)
    {
        var phone = new PersonPhone(type, countryCode, number);

        Phones = [.. Phones, phone];

        return phone;
    }

    public void RemovePhone(PersonPhone contact)
    {
        Phones = Phones
            .Where(c => c.Id != contact.Id)
            .ToList();
    }

    public PersonAddress AddAddress(PersonAddressType type, string postalCode, string street, string neighborhood, string city, string state, string country)
    {
        var address = new PersonAddress(type, postalCode, street, neighborhood, city, state, country);

        Addresses = [.. Addresses, address];

        return address;
    }

    public void RemoveAddress(PersonAddress address)
    {
        Addresses = Addresses
            .Where(a => a.Id != address.Id)
            .ToList();
    }

    protected PersonEmail AddEmail(string mailAddress)
        => AddEmail(mailAddress, false);

    protected PersonEmail AddEmail(string mailAddress, bool principal)
    {
        var email = new PersonEmail(mailAddress);

        if (principal)
        {
            SetPrincipalEmail(email);
        }

        Emails = [.. Emails, email];

        return email;
    }

    public void RemoveEmail(PersonEmail email)
    {
        Emails = Emails
            .Where(c => c.Id != email.Id)
            .ToList();
    }

    public void SetPrincipalEmail(string mailAddress)
        => SetPrincipalEmail(Emails.First(p => p.MailAddress == mailAddress));

    public void SetPrincipalEmail(PersonEmail email)
    {
        Emails = Emails
            .Select(e =>
            {
                e.SetPrincipal(e.Id == email.Id);
                return e;
            })
            .ToList();
    }
}

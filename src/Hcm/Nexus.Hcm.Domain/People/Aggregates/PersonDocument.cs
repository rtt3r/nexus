using Goal.Domain.Aggregates;

namespace Nexus.Hcm.Domain.People.Aggregates;

public sealed class PersonDocument : Entity
{
    private PersonDocument()
        : base()
    {
    }

    public PersonDocument(Person person, Document document, string value)
    {
        ArgumentNullException.ThrowIfNull(person, nameof(person));
        ArgumentNullException.ThrowIfNull(document, nameof(document));
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        Person = person;
        PersonId = person.Id;
        Document = document;
        DocumentId = document.Id;
        Value = value;
    }

    public string PersonId { get; private set; } = default!;
    public string DocumentId { get; private set; } = default!;
    public string Value { get; private set; } = default!;
    public bool Active { get; private set; } = true;
    public Person Person { get; private set; } = default!;
    public Document Document { get; private set; } = default!;
    public IEnumerable<PersonDocumentAttribute> Attributes { get; private set; } = [];

    public void Activate()
        => Active = true;

    public void Inactivate()
        => Active = false;
}

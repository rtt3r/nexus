using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Persons.Aggregates;

public sealed class PersonDocumentAttribute : Entity
{
    private PersonDocumentAttribute()
        : base()
    {
    }

    public PersonDocumentAttribute(PersonDocument document, DocumentAttribute attribute, string value)
        : this()
    {
        ArgumentNullException.ThrowIfNull(document, nameof(document));
        ArgumentNullException.ThrowIfNull(attribute, nameof(attribute));
        ArgumentNullException.ThrowIfNull(value, nameof(value));

        Document = document;
        DocumentId = document.Id;
        Attribute = attribute;
        AttributeId = attribute.Id;
        Value = value;
    }

    public string Name { get; private set; } = default!;
    public string DocumentId { get; private set; } = default!;
    public string AttributeId { get; private set; } = default!;
    public string Value { get; private set; } = default!;
    public DocumentAttribute Attribute { get; private set; } = default!;
    public PersonDocument Document { get; private set; } = default!;
}

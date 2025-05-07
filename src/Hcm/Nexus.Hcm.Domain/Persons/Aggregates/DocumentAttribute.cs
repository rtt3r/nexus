using Goal.Domain.Aggregates;

namespace Nexus.Hcm.Domain.Persons.Aggregates;

public sealed class DocumentAttribute : Entity
{
    private DocumentAttribute()
        : base()
    {
    }

    public DocumentAttribute(string name, Document document, DocumentAttributeType type)
        : this()
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        ArgumentNullException.ThrowIfNull(document, nameof(document));

        Name = name;
        DocumentId = document.Id;
        Document = document;
        Type = type;
    }

    public string Name { get; private set; } = default!;
    public string DocumentId { get; private set; } = default!;
    public DocumentAttributeType Type { get; private set; } = default!;
    public Document Document { get; private set; } = default!;
}

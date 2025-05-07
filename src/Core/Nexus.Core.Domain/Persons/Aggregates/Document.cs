using Goal.Domain.Aggregates;

namespace Nexus.Core.Domain.Persons.Aggregates;

public sealed class Document : Entity
{
    private Document()
        : base()
    {
    }

    public Document(string name)
        : this()
    {
        SetName(name);
    }

    public string Name { get; private set; } = default!;
    public IEnumerable<PersonDocument> PersonDocuments { get; private set; } = new List<PersonDocument>();
    public IEnumerable<DocumentAttribute> Attributes { get; private set; } = new List<DocumentAttribute>();

    public void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        Name = name;
    }
}

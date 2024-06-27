using Nexus.Core.Domain.Shared;

namespace Nexus.Core.Domain.People.Aggregates;

public class PersonDocumentType : EntityTypeDescriptor
{
    protected PersonDocumentType()
        : base()
    {
    }

    public PersonDocumentType(string name)
        : base(name)
    {
    }

    public IEnumerable<PersonDocument> Documents { get; private set; } = [];
}

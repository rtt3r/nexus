using Nexus.Core.Domain.Shared;

namespace Nexus.Core.Domain.People.Aggregates;

public class PersonContactType : EntityTypeDescriptor
{
    protected PersonContactType()
        : base()
    {
    }

    public PersonContactType(string name)
        : base(name)
    {
    }

    public IEnumerable<PersonContact> Contacts { get; private set; } = [];
}
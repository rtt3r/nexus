namespace Nexus.Core.Domain.Persons.Aggregates;

public class LegalEntity : Person
{
    protected LegalEntity()
        : base()
    {
    }

    public LegalEntity(string companyName, string brandName, string cnpj)
        : base(PersonType.Legal, companyName)
    {
        SetBrandName(brandName);
        AddDocument(DocumentType.Cnpj, cnpj);
    }

    public string BrandName { get; protected set; } = default!;

    public virtual void SetCompanyName(string companyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(companyName, nameof(companyName));
        Name = companyName;
    }

    public virtual void SetBrandName(string brandName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(brandName, nameof(brandName));
        BrandName = brandName;
    }
}

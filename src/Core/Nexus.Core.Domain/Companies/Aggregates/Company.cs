using Nexus.Core.Domain.Persons.Aggregates;

namespace Nexus.Core.Domain.Companies.Aggregates;

public sealed class Company : LegalEntity
{
    public string? HeadquartersId { get; private set; }
    public CompanyType CompanyType { get; private set; } = default!;
    public string? Logo { get; private set; }
    public Company? Headquarters { get; private set; } = default!;
    public IList<Company> Branches { get; private set; } = [];
    public IList<UserCompany> Users { get; private set; } = [];

    private Company()
        : base()
    {
    }

    public Company(string companyName, string brandingName, string cnpj)
        : base(companyName, brandingName, cnpj)
    {
        CompanyType = CompanyType.Headquarters;
    }

    public Company(Company headquarters, string companyName, string brandingName, string cnpj)
        : base(companyName, brandingName, cnpj)
    {
        CompanyType = CompanyType.Branch;
        SetHeadquarters(headquarters);
    }

    public void SetHeadquarters(Company headquarters)
    {
        ArgumentNullException.ThrowIfNull(headquarters, nameof(headquarters));

        Headquarters = headquarters;
        HeadquartersId = headquarters.Id;
    }

    public void SetLogo(string logo)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(logo, nameof(logo));
        Logo = logo;
    }
}

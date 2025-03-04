using Nexus.Core.Domain.Persons.Aggregates;

namespace Nexus.Core.Domain.BusinessGroups.Aggregates;

public sealed class Company : LegalEntity
{
    public string BusinessGroupId { get; private set; } = default!;
    public string? HeadquartersId { get; private set; }
    public CompanyType CompanyType { get; private set; } = default!;
    public string? BranchCode { get; private set; }
    public Company Headquarters { get; private set; } = default!;
    public BusinessGroup BusinessGroup { get; private set; } = default!;
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

    public Company(Company headquarters, string companyName, string brandingName, string cnpj, string branchCode)
        : base(companyName, brandingName, cnpj)
    {
        CompanyType = CompanyType.Branch;

        SetHeadquarters(headquarters);
        SetBranchCode(branchCode);
    }

    private void SetHeadquarters(Company headquarters)
    {
        ArgumentNullException.ThrowIfNull(headquarters, nameof(headquarters));

        Headquarters = headquarters;
        HeadquartersId = headquarters.Id;
    }

    public void SetBranchCode(string branchCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(branchCode, nameof(branchCode));
        BranchCode = branchCode;
    }
}

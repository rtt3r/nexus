using Nexus.Core.Model.People;

namespace Nexus.Core.Model.Companies;

public class Company
{
    public string CompanyId { get; set; } = default!;
    public string CompanyName { get; set; } = default!;
    public string BrandingName { get; set; } = default!;
    public string CompanyType { get; set; } = default!;
    public string Cnpj { get; set; } = default!;
    public string? MunicipalRegistration { get; set; }
    public string? StateRegistration { get; set; }
    public string? HeadquartersId { get; set; }
    public string? Logo { get; set; }
    public IList<Company> Branches { get; set; } = [];
    public IList<Contact> Contacts { get; set; } = [];
    public IList<Address> Addresses { get; set; } = [];
}

using Nexus.Core.Web.Features.Companies.GetCompany;

namespace Nexus.Core.Web.Features.Companies.SearchCompanies;

public class SearchCompanyResponse
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
    public IList<SearchCompanyResponse> Branches { get; set; } = [];
    public IList<SearchCompanyContactResponse> Contacts { get; set; } = [];
    public IList<SearchCompanyAddressResponse> Addresses { get; set; } = [];
}

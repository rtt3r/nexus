using Nexus.Core.Api.Features.Companies.CreateCompany;

namespace Nexus.Core.Api.Features.Companies.GetCompany;

public class GetCompanyResponse
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
    public IList<GetCompanyResponse> Branches { get; set; } = [];
    public IList<GetCompanyContactResponse> Contacts { get; set; } = [];
    public IList<GetCompanyAddressResponse> Addresses { get; set; } = [];
}

namespace Nexus.Core.Web.Features.Companies.CreateCompany;

public class CreateCompanyResponse
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
    public IList<CreateCompanyResponse> Branches { get; set; } = [];
    public IList<CreateCompanyContactResponse> Contacts { get; set; } = [];
    public IList<CreateCompanyAddressResponse> Addresses { get; set; } = [];
}

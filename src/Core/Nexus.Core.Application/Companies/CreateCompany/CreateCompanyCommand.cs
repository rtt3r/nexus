using Goal.Application.Commands;
using Nexus.Core.Model.Companies;
using Nexus.Infra.Crosscutting.Errors;
using OneOf;

namespace Nexus.Core.Application.Companies.CreateCompany;

public class CreateCompanyCommand : ICommand<OneOf<Company, AppError>>
{
    public string CompanyName { get; set; } = default!;
    public string BrandingName { get; set; } = default!;
    public string Cnpj { get; set; } = default!;
    public string? StateRegistration { get; set; }
    public string? MunicipalRegistration { get; set; }
    public string? Logo { get; set; }
    public CreateCompanyAddressCommand Address { get; set; } = default!;
    public IList<CreateCompanyContactCommand> Contacts { get; set; } = [];

    public sealed class CreateCompanyAddressCommand
    {
        public string ZipCode { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string Number { get; set; } = default!;
        public string? Complement { get; set; }
        public string Neighborhood { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string Country { get; set; } = default!;
    }

    public sealed class CreateCompanyContactCommand
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string LandlinePhone { get; set; } = default!;
        public string MobilePhone { get; set; } = default!;
        public string Whatsapp { get; set; } = default!;
    }
}

using Nexus.Core.Application.Companies.CreateCompany;

namespace Nexus.Core.Api.Features.Companies.CreateCompany;

public class CreateCompanyRequest
{
    public string CompanyName { get; set; } = default!;
    public string BrandingName { get; set; } = default!;
    public string Cnpj { get; set; } = default!;
    public string? StateRegistration { get; set; }
    public string? MunicipalRegistration { get; set; }
    public string? Logo { get; set; }
    public CreateCompanyAddressRequest Address { get; set; } = default!;
    public IList<CreateCompanyContactRequest> Contacts { get; set; } = [];

    public CreateCompanyCommand ToCommand()
    {
        return new CreateCompanyCommand
        {
            CompanyName = CompanyName,
            BrandingName = BrandingName,
            Cnpj = Cnpj,
            StateRegistration = StateRegistration,
            MunicipalRegistration = MunicipalRegistration,
            Logo = Logo,
            Address = new CreateCompanyCommand.CreateCompanyAddressCommand
            {
                ZipCode = Address.ZipCode,
                City = Address.City,
                Complement = Address.Complement,
                Country = Address.Country,
                Neighborhood = Address.Neighborhood,
                Number = Address.Number,
                State = Address.State,
                Street = Address.Street
            },
            Contacts = [
                ..Contacts.Select(c => new CreateCompanyCommand.CreateCompanyContactCommand
                {
                    Name = c.Name,
                    Email = c.Email,
                    LandlinePhone = c.LandlinePhone,
                    MobilePhone = c.MobilePhone,
                    Whatsapp = c.Whatsapp
                })
            ],
        };
    }

    public sealed class CreateCompanyAddressRequest
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

    public sealed class CreateCompanyContactRequest
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string LandlinePhone { get; set; } = default!;
        public string MobilePhone { get; set; } = default!;
        public string Whatsapp { get; set; } = default!;
    }
}

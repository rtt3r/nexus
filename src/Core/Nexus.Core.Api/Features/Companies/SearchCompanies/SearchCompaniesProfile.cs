using AutoMapper;
using Goal.Infra.Crosscutting.Collections;
using Nexus.Core.Api.Features.Companies.GetCompany;
using Nexus.Core.Model.Companies;
using Nexus.Core.Model.People;

namespace Nexus.Core.Api.Features.Companies.SearchCompanies;

public class SearchCompaniesProfile : Profile
{
    public SearchCompaniesProfile()
    {
        CreateMap<IPagedList<Company>, IPagedList<SearchCompanyResponse>>()
            .ConstructUsing((source, context) =>
            {
                return new PagedList<SearchCompanyResponse>(
                    context.Mapper.Map<List<SearchCompanyResponse>>(source),
                    source.TotalCount);
            });

        CreateMap<Company, SearchCompanyResponse>();
        CreateMap<Address, SearchCompanyAddressResponse>();
        CreateMap<Contact, SearchCompanyContactResponse>();
    }
}

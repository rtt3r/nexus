using AutoMapper;
using Goal.Infra.Crosscutting.Collections;
using Nexus.Hcm.Model.People;

namespace Nexus.Hcm.Api.Features.Employees.SearchEmployees;

public class SearchEmployeesProfile : Profile
{
    public SearchEmployeesProfile()
    {
        CreateMap<IPagedList<Employee>, IPagedList<SearchEmployeeResponse>>()
            .ConstructUsing((source, context) =>
            {
                return new PagedList<SearchEmployeeResponse>(
                    context.Mapper.Map<List<SearchEmployeeResponse>>(source),
                    source.TotalCount);
            });

        CreateMap<Employee, SearchEmployeeResponse>();
        CreateMap<Address, SearchEmployeeAddressResponse>();
        CreateMap<Contact, SearchEmployeeContactResponse>();
    }
}

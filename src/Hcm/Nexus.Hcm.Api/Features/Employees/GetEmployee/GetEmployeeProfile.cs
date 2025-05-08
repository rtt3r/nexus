using AutoMapper;
using Nexus.Hcm.Model.People;

namespace Nexus.Hcm.Api.Features.Employees.GetEmployee;

public class GetEmployeeProfile : Profile
{
    public GetEmployeeProfile()
    {
        CreateMap<Employee, GetEmployeeResponse>();
        CreateMap<Address, GetEmployeeAddressResponse>();
        CreateMap<Contact, GetEmployeeContactResponse>();
    }
}

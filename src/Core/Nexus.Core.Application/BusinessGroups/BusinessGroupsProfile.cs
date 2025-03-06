using AutoMapper;

namespace Nexus.Core.Application.BusinessGroups;

internal class BusinessGroupsProfile : Profile
{
    public BusinessGroupsProfile()
    {
        CreateMap<Domain.BusinessGroups.Aggregates.BusinessGroup, Model.BusinessGroups.BusinessGroup>();
    }
}

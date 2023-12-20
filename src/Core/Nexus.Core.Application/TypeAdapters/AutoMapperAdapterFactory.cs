using AutoMapper;
using Goal.Seedwork.Infra.Crosscutting.Adapters;

namespace Nexus.Core.Application.TypeAdapters;

public class AutoMapperAdapterFactory(IMapper mapper) : ITypeAdapterFactory
{
    private readonly IMapper mapper = mapper;

    public ITypeAdapter Create()
        => new AutoMapperAdapter(mapper);
}

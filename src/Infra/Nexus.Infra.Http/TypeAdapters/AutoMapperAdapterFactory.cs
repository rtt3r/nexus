using AutoMapper;
using Goal.Infra.Crosscutting.Adapters;

namespace Nexus.Infra.Http.TypeAdapters;

public sealed class AutoMapperAdapterFactory(IMapper mapper) : ITypeAdapterFactory
{
    private readonly IMapper mapper = mapper;

    public ITypeAdapter Create()
        => new AutoMapperAdapter(mapper);
}

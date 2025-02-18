using AutoMapper;
using Goal.Infra.Crosscutting.Adapters;

namespace Nexus.Core.Application.TypeAdapters;

internal class AutoMapperAdapterFactory(IMapper mapper) : ITypeAdapterFactory
{
    private readonly IMapper mapper = mapper;

    public ITypeAdapter Create()
        => new AutoMapperAdapter(mapper);
}

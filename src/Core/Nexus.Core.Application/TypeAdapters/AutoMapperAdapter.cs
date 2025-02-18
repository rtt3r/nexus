using AutoMapper;
using Goal.Infra.Crosscutting.Adapters;

namespace Nexus.Core.Application.TypeAdapters;

internal class AutoMapperAdapter(IMapper mapper) : ITypeAdapter
{
    private readonly IMapper mapper = mapper;

    public TTarget Adapt<TSource, TTarget>(TSource source)
        => mapper.Map<TSource, TTarget>(source);

    public TTarget Adapt<TTarget>(object source)
        => mapper.Map<TTarget>(source);
}

using AutoMapper;
using Goal.Seedwork.Infra.Crosscutting.Adapters;

namespace Nexus.Core.Application.TypeAdapters;

public class AutoMapperAdapter(IMapper mapper) : ITypeAdapter
{
    private readonly IMapper mapper = mapper;

    public TTarget Adapt<TSource, TTarget>(TSource source)
        => mapper.Map<TSource, TTarget>(source);

    public TTarget Adapt<TTarget>(object source)
        => mapper.Map<TTarget>(source);
}

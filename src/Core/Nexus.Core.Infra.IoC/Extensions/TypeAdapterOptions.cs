using System.Reflection;

namespace Nexus.Core.Infra.IoC.Extensions;

public sealed class TypeAdapterOptions
{
    public Assembly[] AutoMapperAssemblies { get; private set; } = [];

    public void RegisterAutoMapperAssemblies(params Assembly[] assemblies)
        => AutoMapperAssemblies = assemblies ?? [];
}
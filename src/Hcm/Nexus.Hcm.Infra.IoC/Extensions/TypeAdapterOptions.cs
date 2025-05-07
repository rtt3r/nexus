using System.Reflection;

namespace Nexus.Hcm.Infra.IoC.Extensions;

public sealed class TypeAdapterOptions
{
    public Assembly[] AutoMapperAssemblies { get; private set; } = [];

    public void RegisterAutoMapperAssemblies(params Assembly[] assemblies)
        => AutoMapperAssemblies = assemblies ?? [];
}
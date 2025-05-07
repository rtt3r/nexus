using System.Reflection;

namespace Nexus.Hcm.Application.Extensions.DependencyInjection;

public sealed class HcmApplicationOptions
{
    public Assembly[] MediatRAssemblies { get; private set; } = [];

    public void RegisterMediatRFromAssemblies(params Assembly[] assemblies) => MediatRAssemblies = assemblies ?? [];
}

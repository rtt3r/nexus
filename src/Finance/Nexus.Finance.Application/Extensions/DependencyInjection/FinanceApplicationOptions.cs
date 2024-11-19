using System.Reflection;

namespace Nexus.Finance.Application.Extensions.DependencyInjection;

public sealed class FinanceApplicationOptions
{
    public Assembly[] MediatRAssemblies { get; private set; } = [];

    public void RegisterMediatRFromAssemblies(params Assembly[] assemblies) => MediatRAssemblies = assemblies ?? [];

}

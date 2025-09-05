using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetLibraries.Scrutor;
public interface ITypeSourceSelector : IAssemblySelector
{
}

public interface IImplementationTypeSelector
{
    IServiceTypeSelector AddClasses(bool? publicOnly);
}

public interface IAssemblySelector
{
    IImplementationTypeSelector FromAssemblies(params Assembly[] assemblies);
}

public interface IServiceTypeSelector
{
    IServiceTypeSelector UsingRegistrationStrategy(RegistrationStrategy registrationStrategy = RegistrationStrategy.Skip);

    ILifeTimeSelector AsMatchingInterface();
}

public interface ILifeTimeSelector
{
    IImplementationTypeSelector WithScopedLifetime();
}

public static class Extensions
{
    public static void Scan(this IServiceCollection services, Action<ITypeSourceSelector> action)
    {

    }
}


public enum RegistrationStrategy
{
    Replace,
    Skip
}
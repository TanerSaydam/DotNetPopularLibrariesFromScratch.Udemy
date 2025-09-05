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
        var selector = new ScrutorSelector();
        action(selector);
        selector.Execute();
    }
}


public enum RegistrationStrategy
{
    Replace,
    Skip
}

public sealed class ScrutorSelector : ITypeSourceSelector, IImplementationTypeSelector, IServiceTypeSelector, ILifeTimeSelector
{
    public IImplementationTypeSelector FromAssemblies(params Assembly[] assemblies)
    {
        throw new NotImplementedException();
    }
    public IServiceTypeSelector AddClasses(bool? publicOnly)
    {
        throw new NotImplementedException();
    }

    public IServiceTypeSelector UsingRegistrationStrategy(RegistrationStrategy registrationStrategy = RegistrationStrategy.Skip)
    {
        throw new NotImplementedException();
    }

    public ILifeTimeSelector AsMatchingInterface()
    {
        throw new NotImplementedException();
    }

    public IImplementationTypeSelector WithScopedLifetime()
    {
        throw new NotImplementedException();
    }

    public void Execute()
    {

    }
}

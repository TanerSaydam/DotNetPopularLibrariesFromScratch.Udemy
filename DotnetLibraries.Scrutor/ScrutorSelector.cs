using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetLibraries.Scrutor;

public sealed class ScrutorSelector(
    IServiceCollection services) : ITypeSourceSelector, IImplementationTypeSelector, IServiceTypeSelector, ILifeTimeSelector
{
    private Assembly[] _assemblies = [];
    private bool _publicOnly = false;
    private RegistrationStrategy _registrationStrategy;
    private bool _isMatchingInterface;
    private ServiceLifetime _lifetime;
    public IImplementationTypeSelector FromAssemblies(params Assembly[] assemblies)
    {
        _assemblies = assemblies;
        return this;
    }
    public IServiceTypeSelector AddClasses(bool publicOnly = false)
    {
        _publicOnly = publicOnly;
        return this;
    }

    public IServiceTypeSelector UsingRegistrationStrategy(RegistrationStrategy registrationStrategy = RegistrationStrategy.Skip)
    {
        _registrationStrategy = registrationStrategy;
        return this;
    }

    public ILifeTimeSelector AsMatchingInterface()
    {
        _isMatchingInterface = true;
        return this;
    }

    public IImplementationTypeSelector WithScopedLifetime()
    {
        _lifetime = ServiceLifetime.Scoped;
        return this;
    }

    public void Execute()
    {
        var types = _assemblies
            .SelectMany(i => i.GetTypes())
            .Where(p => p.IsClass && !p.IsAbstract && p.GetInterfaces().Any());

        if (_publicOnly)
        {
            types = types.Where(p => p.IsPublic);
        }

        foreach (var classType in types)
        {
            var interfaceType = classType;
            if (_isMatchingInterface)
            {
                interfaceType = classType
                    .GetInterfaces()
                    .FirstOrDefault(p => string.Equals(p.Name, "I" + classType.Name, StringComparison.Ordinal));
                if (interfaceType is null) continue;
            }

            if (_registrationStrategy == RegistrationStrategy.Skip)
            {
                bool alreadyRegistered = services
                    .Any(p => p.ServiceType == interfaceType && p.ImplementationType == classType);
                if (alreadyRegistered) continue;
            }

            services.Add(new ServiceDescriptor(interfaceType, classType, _lifetime));
        }
    }
}

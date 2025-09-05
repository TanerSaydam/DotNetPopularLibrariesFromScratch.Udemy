using System.Reflection;

namespace DotnetLibraries.Scrutor;

public interface IAssemblySelector
{
    IImplementationTypeSelector FromAssemblies(params Assembly[] assemblies);
}

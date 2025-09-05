namespace DotnetLibraries.Scrutor;

public interface ILifeTimeSelector
{
    IImplementationTypeSelector WithScopedLifetime();
}

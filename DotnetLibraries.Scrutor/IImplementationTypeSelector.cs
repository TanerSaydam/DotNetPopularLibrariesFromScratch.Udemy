namespace DotnetLibraries.Scrutor;

public interface IImplementationTypeSelector
{
    IServiceTypeSelector AddClasses(bool publicOnly = false);
}

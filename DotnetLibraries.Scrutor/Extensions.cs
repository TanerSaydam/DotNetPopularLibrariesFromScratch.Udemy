using Microsoft.Extensions.DependencyInjection;

namespace DotnetLibraries.Scrutor;

public static class Extensions
{
    public static void Scan(this IServiceCollection services, Action<ITypeSourceSelector> action)
    {
        var selector = new ScrutorSelector(services);
        action(selector);
        selector.Execute();
    }
}

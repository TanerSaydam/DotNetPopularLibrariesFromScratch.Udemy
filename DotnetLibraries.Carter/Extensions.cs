using System.Reflection;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetLibraries.Carter;

public static class Extensions
{
    public static void AddCarter(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        var modules = assembly.GetTypes()
            .Where(p => p.IsClass && !p.IsAbstract && typeof(ICarterModule).IsAssignableFrom(p));
        foreach (var module in modules)
        {
            services.AddTransient(typeof(ICarterModule), module);
        }
    }
    public static void MapCarter(this IEndpointRouteBuilder builder)
    {
        var modules = builder.ServiceProvider.GetServices<ICarterModule>();
        foreach (var module in modules)
        {
            module.AddRoutes(builder);
        }
    }
}

using DotnetLibraries.Carter;

namespace DotnetLibraries.WebAPI.Modules;

public class CategoryModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder builder)
    {
        var app = builder.MapGroup("categories");

        app.MapGet(string.Empty, () =>
        {
            return "Hello World";
        });
    }
}

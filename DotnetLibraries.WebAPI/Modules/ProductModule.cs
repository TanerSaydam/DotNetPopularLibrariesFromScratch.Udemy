using Carter;
using DotnetLibraries.WebAPI.Context;

namespace DotnetLibraries.WebAPI.Modules;

public class ProductModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder builder)
    {
        var app = builder.MapGroup("products");

        app.MapGet(string.Empty, (ApplicationDbContext dbContext) =>
        {
            var res = dbContext.Products.ToList();
            return res;
        });
    }
}
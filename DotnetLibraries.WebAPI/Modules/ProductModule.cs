using DotnetLibraries.Carter;
using DotnetLibraries.WebAPI.Context;
using DotnetLibraries.WebAPI.Models;

namespace DotnetLibraries.WebAPI.Modules;

public class ProductModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder builder)
    {
        var app = builder.MapGroup("products");

        app.MapGet(string.Empty, (IProductService productService) =>
        {
            var res = productService.GetAll();
            return res;
        });
    }
}

public interface IProductService
{
    List<Product> GetAll();
}

public sealed class ProductService(ApplicationDbContext dbContext) : IProductService
{
    public List<Product> GetAll()
    {
        return dbContext.Products.ToList();
    }
}

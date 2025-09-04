using Microsoft.AspNetCore.Routing;

namespace DotnetLibraries.Carter;
public interface ICarterModule
{
    void AddRoutes(IEndpointRouteBuilder builder);
}
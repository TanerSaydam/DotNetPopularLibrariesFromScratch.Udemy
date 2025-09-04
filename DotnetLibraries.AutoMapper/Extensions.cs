using Microsoft.Extensions.DependencyInjection;

namespace DotnetLibraries.AutoMapper;
public static class Extensions
{
    public static void AddAutoMapper(
        this IServiceCollection services,
        Action<AutoMapperConfiguration> configuration)
    {
        services.AddScoped<IMapper, Mapper>();
    }
}
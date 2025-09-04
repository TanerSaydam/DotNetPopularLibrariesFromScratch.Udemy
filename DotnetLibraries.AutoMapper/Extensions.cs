using Microsoft.Extensions.DependencyInjection;

namespace DotnetLibraries.AutoMapper;
public static class Extensions
{
    public static void AddAutoMapper(
        this IServiceCollection services,
        Action<AutoMapperConfiguration> configuration)
    {
        var cfg = new AutoMapperConfiguration();
        configuration.Invoke(cfg);
        services.AddSingleton(cfg);
        services.AddScoped<IMapper, Mapper>();
    }
}
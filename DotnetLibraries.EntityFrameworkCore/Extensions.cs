using Microsoft.Extensions.DependencyInjection;

namespace DotnetLibraries.EntityFrameworkCore;

public static class Extensions
{
    public static void AddDbContext<TContext>(
        this IServiceCollection services,
        Action<DbContextOpitonsBuilder> action)
        where TContext : DbContext
    {
        services.AddScoped(sp =>
        {
            var dbContextBuilder = new DbContextOpitonsBuilder();
            action(dbContextBuilder);
            var options = dbContextBuilder.Options;
            return (TContext)Activator.CreateInstance(typeof(TContext), options)!;
        });
    }
}

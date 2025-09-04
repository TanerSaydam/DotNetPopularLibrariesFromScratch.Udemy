using Microsoft.Extensions.DependencyInjection;

namespace DotnetLibraries.EntityFrameworkCore;
public class DbContext
{
    public DbContext(DbContextOptions options)
    {

    }
}

public sealed class DbContextOptions
{
    public string ConnectionString { get; set; } = default!;

}

public sealed class DbSet<TEntity>
    where TEntity : class, new()
{
    public List<TEntity> ToList()
    {

        return new();
    }
}

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

public sealed class DbContextOpitonsBuilder
{
    public DbContextOptions Options { get; set; } = new();
    public void UseSqlServer(string connectionString)
    {
        Options.ConnectionString = connectionString;
    }
}

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetLibraries.EntityFrameworkCore;
public class DbContext
{
    public DbContextOptions Options { get; set; }
    public DbContext(DbContextOptions options)
    {
        Options = options;

        var dbSetProps = this.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p =>
            p.PropertyType.IsGenericType
            && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

        foreach (var prop in dbSetProps)
        {
            var entityType = prop.PropertyType.GetGenericArguments()[0];
            var instance = Activator.CreateInstance(
                typeof(DbSet<>).MakeGenericType(entityType),
                this,
                prop.Name);
        }

    }
}

public sealed class DbContextOptions
{
    public string ConnectionString { get; set; } = default!;

}

public sealed class DbSet<TEntity>
    where TEntity : class, new()
{
    private readonly string _connectinString;
    private readonly string _tableName;
    public DbSet(DbContext dbContext, string tableName)
    {
        _connectinString = dbContext.Options.ConnectionString;
        _tableName = tableName;
    }
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

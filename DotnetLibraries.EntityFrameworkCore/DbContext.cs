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

    }
}

public sealed class DbContextOpitonsBuilder
{
    public void UseSqlServer(string connectionString)
    {

    }
}

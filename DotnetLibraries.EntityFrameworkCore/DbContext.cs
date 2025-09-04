using System.Reflection;

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
            prop.SetValue(this, instance);
        }

    }
}
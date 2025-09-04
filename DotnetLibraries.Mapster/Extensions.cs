using System.Reflection;

namespace DotnetLibraries.Mapster;
public static class Extensions
{
    private static TEntity MapTo<TEntity>(object src, TEntity instance) where TEntity : class, new()
    {
        var srcProperties = src.GetType().GetProperties(
                    BindingFlags.Public | BindingFlags.Instance);
        var destProperties = instance.GetType().GetProperties(
            BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanWrite)
            .ToList();

        foreach (var srcProperty in srcProperties)
        {
            var destProperty = destProperties.FirstOrDefault(p => p.Name == srcProperty.Name);
            if (destProperty is null) continue;
            if (destProperty.PropertyType != srcProperty.PropertyType) continue;

            destProperty.SetValue(instance, srcProperty.GetValue(src));
        }

        return instance;
    }
    public static TEntity Adapt<TEntity>(this object src)
        where TEntity : class, new()
    {
        var instance = new TEntity();
        return MapTo(src, instance);
    }

    public static TEntity Adapt<TEntity>(this object src, TEntity dest)
        where TEntity : class, new()
    {

        return MapTo(src, dest);
    }
}

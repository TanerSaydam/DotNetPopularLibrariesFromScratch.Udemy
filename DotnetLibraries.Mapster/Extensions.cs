using System.Collections.Concurrent;
using System.Reflection;

namespace DotnetLibraries.Mapster;
public static class Extensions
{
    private static readonly ConcurrentDictionary<(Type src, Type dest), Action<object, object>> _cache = new();

    private static Action<object, object> Get(Type src, Type dest)
        => _cache.GetOrAdd((src, dest), Build);

    private static Action<object, object> Build((Type src, Type dest) key)
    {
        var srcProperties = key.src.GetProperties(
            BindingFlags.Instance | BindingFlags.Public);
        var destProperties = key.dest.GetProperties(
             BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.CanWrite)
            .ToDictionary(p => p.Name);

        var pairs = new List<(PropertyInfo Src, PropertyInfo dest)>();
        foreach (var srcProp in srcProperties)
        {
            if (!destProperties.TryGetValue(srcProp.Name, out var dp)) continue;
            if (!dp.PropertyType.IsAssignableFrom(srcProp.PropertyType)) continue;

            pairs.Add((srcProp, dp));
        }

        return (srcObj, destObj) =>
        {
            foreach (var (sp, dp) in pairs)
            {
                var value = sp.GetValue(srcObj);
                dp.SetValue(destObj, value);
            }
        };
    }
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
        Get(src.GetType(), instance.GetType())(src, instance);
        return instance;
    }
    public static TEntity Adapt<TEntity>(this object src, TEntity dest)
        where TEntity : class, new()
    {
        Get(src.GetType(), dest.GetType())(src, dest);
        return dest;
    }
}
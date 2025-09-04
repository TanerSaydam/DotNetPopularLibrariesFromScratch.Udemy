using System.Collections.Concurrent;
using System.Reflection;

namespace DotnetLibraries.AutoMapper;

public sealed class AutoMapperConfiguration
{
    public string LicenseKey { get; set; } = default!;
    private readonly ConcurrentDictionary<(Type Src, Type Dest), Action<object, object>> _maps = new();

    public void MapTo<TSoruce, TDestination>(TSoruce srcObj, TDestination destObj)
    {
        if (!_maps.TryGetValue((srcObj.GetType(), destObj.GetType()), out var action))
        {
            throw new InvalidOperationException($"Mapping not found: {srcObj.GetType().Name} to {destObj.GetType().Name}");
        }

        action(srcObj, destObj);
    }
    public void CreateMap<TSource, TDestination>()
    {
        var action = Build(typeof(TSource), typeof(TDestination));
        _maps.TryAdd((typeof(TSource), typeof(TDestination)), action);
    }

    private Action<object, object> Build(Type src, Type dest)
    {
        var srcProperties = src.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var destProperties = dest.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanWrite)
            .ToDictionary(p => p.Name);

        List<(PropertyInfo SrcProp, PropertyInfo DestProp)> pairs = new();
        foreach (var srcProperty in srcProperties)
        {
            if (!destProperties.TryGetValue(srcProperty.Name, out var dp)) continue;
            if (dp.PropertyType != srcProperty.PropertyType) continue;

            pairs.Add((srcProperty, dp));
        }

        return (srcObj, destObj) =>
        {
            foreach (var item in pairs)
            {
                var value = item.SrcProp.GetValue(srcObj);
                item.DestProp.SetValue(destObj, value);
            }
        };
    }
}

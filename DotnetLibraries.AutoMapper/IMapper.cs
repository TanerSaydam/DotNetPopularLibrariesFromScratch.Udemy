namespace DotnetLibraries.AutoMapper;
public interface IMapper
{
    TDestination Map<TDestination>(object source)
        where TDestination : class, new();

    TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
}

internal sealed class Mapper(AutoMapperConfiguration cfg) : IMapper
{
    public TDestination Map<TDestination>(object source)
        where TDestination : class, new()
    {
        var destination = new TDestination();
        cfg.MapTo(source, destination);
        return destination;
    }

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        cfg.MapTo(source, destination);
        return destination;
    }
}
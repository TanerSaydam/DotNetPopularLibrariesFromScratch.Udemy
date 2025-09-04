namespace DotnetLibraries.AutoMapper;
public interface IMapper
{
    TDestination Map<TDestination>(object source)
        where TDestination : class, new();
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
}
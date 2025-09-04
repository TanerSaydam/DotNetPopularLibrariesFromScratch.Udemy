namespace DotnetLibraries.AutoMapper;
public interface IMapper
{
    TDestination Map<TDestination>(object source)
        where TDestination : class, new();
}

internal sealed class Mapper : IMapper
{
    public TDestination Map<TDestination>(object source)
        where TDestination : class, new()
    {
        var destination = new TDestination();
        return destination;
    }
}
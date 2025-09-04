namespace DotnetLibraries.WebAPI.Dtos;

public sealed record CreateProductDto(
    string Name,
    decimal Price);

public sealed record UpdateProductDto(
    string Name);

using DotnetLibraries.WebAPI.Attributes;
using DotnetLibraries.WebAPI.Dtos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/products", (CreateProductDto request) =>
{
    return Results.Ok(new { message = "Product create is successful" });
}).AddEndpointFilter<ValidateFilter>();

app.MapControllers();

app.Run();
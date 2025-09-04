using DotnetLibraries.AutoMapper;
using DotnetLibraries.Carter;
using DotnetLibraries.EntityFrameworkCore;
using DotnetLibraries.WebAPI.Context;
using DotnetLibraries.WebAPI.Dtos;
using DotnetLibraries.WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(action =>
{
    action.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EFCoreDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.LicenseKey = "";
    cfg.CreateMap<CreateProductDto, Product>();
    cfg.CreateMap<UpdateProductDto, Product>();
});

builder.Services.AddCarter();

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapCarter();

app.MapControllers();

app.Run();
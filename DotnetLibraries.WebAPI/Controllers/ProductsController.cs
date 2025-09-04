using DotnetLibraries.Mapster;
using DotnetLibraries.WebAPI.Attributes;
using DotnetLibraries.WebAPI.Dtos;
using DotnetLibraries.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetLibraries.WebAPI.Controllers;

[ApiController]
[Route("/api/products")]
[Validate]
public sealed class ProductsController : ControllerBase
{
    [HttpPost]
    public IActionResult Create(CreateProductDto request)
    {
        var product = request.Adapt<Product>();
        return Ok(new { Message = "Create product is successful" });
    }

    [HttpPut]
    public IActionResult Update(UpdateProductDto request)
    {
        var product = new Product();

        //request.Adapt(product);

        return Ok(new { Message = "Create product is successful" });
    }
}
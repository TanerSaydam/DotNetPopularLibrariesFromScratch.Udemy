using DotnetLibraries.WebAPI.Attributes;
using DotnetLibraries.WebAPI.Dtos;
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
        return Ok(new { Message = "Create product is successful" });
    }
}
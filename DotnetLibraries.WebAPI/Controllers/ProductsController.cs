using DotnetLibraries.FluentValidation;
using DotnetLibraries.WebAPI.Dtos;
using DotnetLibraries.WebAPI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace DotnetLibraries.WebAPI.Controllers;

[ApiController]
[Route("/api/products")]
public sealed class ProductsController : ControllerBase
{
    [HttpPost]
    public IActionResult Create(CreateProductDto request)
    {
        CreateProductValidator validator = new();
        ValidationResult validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            var message = validationResult.Errors;
            return BadRequest(message);
        }

        return Ok(new { Message = "Create product is successful" });
    }
}
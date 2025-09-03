using DotnetLibraries.FluentValidation;
using DotnetLibraries.WebAPI.Dtos;

namespace DotnetLibraries.WebAPI.Validators;

public sealed class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name).NotEmpty();//.WithMessage("This is a custom message for name");
        RuleFor(p => p.Price).NotEmpty();//.GreaterThan(0).WithMessage("This is a custom message for price");
    }
} //lambda expression

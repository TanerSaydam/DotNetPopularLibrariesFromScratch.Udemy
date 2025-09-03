using System.Reflection;
using DotnetLibraries.FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotnetLibraries.WebAPI.Attributes;

public sealed class ValidateAttribute : Attribute, IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context) { }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var body = context.ActionArguments.FirstOrDefault().Value;
        if (body is null) return;

        var bodyType = body.GetType();
        var validatorBaseType = typeof(AbstractValidator<>).MakeGenericType(bodyType);

        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes()
            .Where(i => i.IsClass
            && !i.IsAbstract
            && validatorBaseType.IsAssignableFrom(i)
            );

        List<ValidationResult> results = new();
        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            if (instance is null) continue;

            MethodInfo? methodInfo = type.GetMethod("Validate");
            if (methodInfo is null) continue;
            ValidationResult result = (ValidationResult)methodInfo.Invoke(instance, [body])!;
            if (!result.IsValid) results.Add(result);
        }

        if (results.Any())
        {
            context.Result = new ObjectResult(results.SelectMany(i => i.Errors).Distinct())
            {
                StatusCode = 422
            };

            return;
        }
    }
}

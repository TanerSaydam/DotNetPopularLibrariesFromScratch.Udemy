
using System.Reflection;
using DotnetLibraries.FluentValidation;

namespace DotnetLibraries.WebAPI.Attributes;

public sealed class ValidateFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var body = context.Arguments.FirstOrDefault();
        if (body is null) return await next(context);

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
            return Results.BadRequest(results.SelectMany(i => i.Errors).Distinct());
        }

        return await next(context);
    }
}

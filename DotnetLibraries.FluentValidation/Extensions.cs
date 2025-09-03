namespace DotnetLibraries.FluentValidation;

public static class Extensions
{
    public static IRuleBuilder<TEntity, string> NotEmpty<TEntity>(this IRuleBuilder<TEntity, string> builder)
    {
        Func<TEntity, ValidationError?> func = instance =>
        {
            var value = (string)builder._getter(instance);
            if (string.IsNullOrEmpty(value))
            {
                return new ValidationError(builder._propertyName, "NotEmpty", "Name cannot be null or empty");
            }

            return null;
        };

        builder._funcs.Add(func!);
        return builder;
    }
}
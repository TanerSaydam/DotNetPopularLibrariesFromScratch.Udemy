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
                string errorMessage = $"{builder._propertyName} cannot be empty or null";
                return new ValidationError(builder._propertyName, "NotEmpty", errorMessage);
            }

            return null;
        };

        builder._funcs.Add(func!);
        return builder;
    }

    public static IRuleBuilder<TEntity, decimal> GreaterThan<TEntity>(this IRuleBuilder<TEntity, decimal> builder, decimal max)
    {
        Func<TEntity, ValidationError?> func = instance =>
        {
            var value = (decimal)builder._getter(instance);
            if (value <= max)
            {
                string errorMessage = $"{builder._propertyName} must be greater than {max}";
                return new ValidationError(builder._propertyName, "GreaterThan", errorMessage);
            }

            return null;
        };

        builder._funcs.Add(func!);

        return builder;
    }

    public static IRuleBuilder<TEntity, TProperty> WithMessage<TEntity, TProperty>(this IRuleBuilder<TEntity, TProperty> builder, string messsage)
    {
        var lastIndex = builder._funcs.Count - 1;
        var lastRule = builder._funcs[lastIndex];

        builder._funcs[lastIndex] = instance =>
        {
            var failure = lastRule(instance);
            if (failure is not null)
            {
                return new(failure.PropertyName, failure.ErrorCode, messsage);
            }

            return null;
        };

        return builder;
    }
}
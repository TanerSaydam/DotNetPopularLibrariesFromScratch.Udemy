namespace DotnetLibraries.FluentValidation;

public static class Extensions
{
    public static IRuleBuilder NotEmpty(this IRuleBuilder builder)
    {
        return builder;
    }
}
using System.Linq.Expressions;

namespace DotnetLibraries.FluentValidation;
public abstract class AbstractValidator<TEntity>
    where TEntity : class
{
    private readonly List<Func<TEntity, object>> _getters = new();
    public IRuleBuilder RuleFor<TProperty>(Expression<Func<TEntity, TProperty>> expression)
    {
        var compile = expression.Compile();
        Func<TEntity, object> getter = x => compile(x)!;
        _getters.Add(getter);

        return new RuleBuilder();
    }

    public ValidationResult Validate(TEntity obj)
    {
        var res = new ValidationResult();
        return res;
    }
}
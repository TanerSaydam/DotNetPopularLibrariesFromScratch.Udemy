using System.Linq.Expressions;

namespace DotnetLibraries.FluentValidation;
public abstract class AbstractValidator<TEntity>
    where TEntity : class
{
    private readonly List<Func<TEntity, object>> _getters = new();
    private readonly List<Func<TEntity, ValidationError>> _funcs = new();
    public IRuleBuilder<TEntity, TProperty> RuleFor<TProperty>(Expression<Func<TEntity, TProperty>> expression)
    {
        var compile = expression.Compile();
        Func<TEntity, object> getter = x => compile(x)!;
        _getters.Add(getter);

        if (expression.Body is not MemberExpression memberExpression)
        {
            throw new ArgumentException();
        }
        string propertyName = memberExpression.Member.Name;

        return new RuleBuilder<TEntity, TProperty>(propertyName, getter, _funcs);
    }

    public ValidationResult Validate(TEntity obj)
    {
        var res = new ValidationResult();
        foreach (var func in _funcs)
        {
            var validationError = func(obj);
            if (validationError is not null)
            {
                res.Errors.Add(validationError);
            }
        }

        return res;
    }
}
using System.Linq.Expressions;

namespace DotnetLibraries.FluentValidation;
public abstract class AbstractValidator<TEntity>
    where TEntity : class
{
    public IRuleBuilder RuleFor<TProperty>(Expression<Func<TEntity, TProperty>> expression)
    {
        return new RuleBuilder();
    }

    public ValidationResult Validate(TEntity obj)
    {
        var res = new ValidationResult();
        return res;
    }
}
using System.Linq.Expressions;

namespace DotnetLibraries.FluentValidation;
public abstract class AbstractValidator<TEntity>
    where TEntity : class
{
    public void RuleFor<TProperty>(Expression<Func<TEntity, TProperty>> expression)
    {
    }

    public ValidationResult Validate(TEntity obj)
    {
        var res = new ValidationResult();
        return res;
    }
}
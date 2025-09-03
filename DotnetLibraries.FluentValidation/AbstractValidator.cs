using System.Linq.Expressions;

namespace DotnetLibraries.FluentValidation;
public abstract class AbstractValidator<TEntity>
    where TEntity : class
{
    public void RuleFor<TProperty>(Expression<Func<TEntity, TProperty>> expression)
    {
    }
}

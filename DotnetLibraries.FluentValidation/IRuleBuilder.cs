namespace DotnetLibraries.FluentValidation;
public interface IRuleBuilder<TEntity>
{
    string _propertyName { get; }
    Func<TEntity, object> _getter { get; }
    List<Func<TEntity, ValidationError>> _funcs { get; }
}

public sealed class RuleBuilder<TEntity> : IRuleBuilder<TEntity>
{
    public Func<TEntity, object> _getter { get; }
    public string _propertyName { get; }
    public List<Func<TEntity, ValidationError>> _funcs { get; }
    public RuleBuilder(string propertyName, Func<TEntity, object> getter, List<Func<TEntity, ValidationError>> funcs)
    {
        _propertyName = propertyName;
        _getter = getter;
        _funcs = funcs;
    }
}

namespace DotnetLibraries.FluentValidation;

public sealed class ValidationResult
{
    public bool IsValid => !Errors.Any();
    public List<ValidationError> Errors { get; set; } = new();
}

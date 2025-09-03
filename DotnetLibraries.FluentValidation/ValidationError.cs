namespace DotnetLibraries.FluentValidation;

public sealed record ValidationError(
    string PropertyName,
    string ErrorCode,
    string ErrorMessage);
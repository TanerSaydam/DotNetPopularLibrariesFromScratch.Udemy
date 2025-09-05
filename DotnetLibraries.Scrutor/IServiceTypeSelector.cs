namespace DotnetLibraries.Scrutor;

public interface IServiceTypeSelector
{
    IServiceTypeSelector UsingRegistrationStrategy(RegistrationStrategy registrationStrategy = RegistrationStrategy.Skip);

    ILifeTimeSelector AsMatchingInterface();
}

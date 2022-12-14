namespace Manganese;

/// <summary>
/// A method guard is an attribute that can be applied to a method to ensure that the method is only called with a
/// specific condition. This method guard checks only with a specific parameter. The parameter must be specified by
/// using <see cref="ParameterSpecification"/> above the guard implementing class.
/// </summary>
public interface ISpecificMethodGuard
{
    /// <summary>
    /// Checks if the given parameter satisfies the guard condition.
    /// </summary>
    /// <param name="parameter">The parameter for the guard to check against.</param>
    /// <returns>True, if the guard is satisfied and can let the method be called.</returns>
    bool IsSatisfied(object? parameter);
}
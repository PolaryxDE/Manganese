namespace Manganese;

/// <summary>
/// A method guard is an attribute that can be applied to a method to ensure that the method is only called with a
/// specific condition. This is a parameterless method guard which will look at all passed parameters instead of a
/// specific parameter. If a check with only a specific parameter is wanted, use the <see cref="ISpecificMethodGuard{T}"/>.
/// </summary>
public interface IMethodGuard
{
    /// <summary>
    /// Checks if the method guard is satisfied by passing the arguments of the method call.
    /// </summary>
    /// <param name="arguments">The arguments which will be passed to the method.</param>
    /// <returns>True, if the guard check succeeded and the method can be called.</returns>
    bool IsSatisfied(object?[] arguments);
}
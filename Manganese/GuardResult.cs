namespace Manganese;

/// <summary>
/// The guard result is returned when the <see cref="GuardRegistry.InvokeGuarded"/> is executed.
/// The result contains if the guard was successful and the result of the guarded action.
/// The guard can be casted to a bool to check if the guard passed.
/// </summary>
public struct GuardResult
{
    /// <summary>
    /// Whether the guard passed or not.
    /// </summary>
    public bool Passed { get; }
    
    /// <summary>
    /// The return value of the guarded action. Definitely null if the guard failed.
    /// </summary>
    public object? Result { get; }
    
    /// <summary>
    /// Overrides the implicit conversion from <see cref="GuardResult"/> to <see cref="bool"/>.
    /// </summary>
    public static implicit operator bool(GuardResult result) => result.Passed;
    
    internal GuardResult(bool passed, object? result = null)
    {
        Passed = passed;
        Result = result;
    }
}
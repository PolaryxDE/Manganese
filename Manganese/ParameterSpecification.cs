namespace Manganese;

/// <summary>
/// Specifies the parameter for a <see cref="ISpecificMethodGuard"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ParameterSpecification : Attribute
{
    /// <summary>
    /// The type of the parameter. (required)!
    /// </summary>
    public Type Type { get; }
    
    /// <summary>
    /// The index of the parameter.
    /// </summary>
    public int? Index { get; }

    public ParameterSpecification(Type type, int? index = null)
    {
        Type = type;
        Index = index;
    }
}
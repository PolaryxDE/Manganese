using System.Reflection;

namespace Manganese;

/// <summary>
/// The guard registry is the main class to access the guarding functionality.
/// </summary>
public class GuardRegistry
{
    /// <summary>
    /// Invokes the given method with the given instance and arguments, if the marked guards where satisfied.
    /// </summary>
    /// <param name="instance">The instance owning the method.</param>
    /// <param name="method">The method to be called.</param>
    /// <param name="arguments">The arguments of the method.</param>
    /// <returns>The <see cref="GuardResult"/> of the invocation.</returns>
    public GuardResult InvokeGuarded(object? instance, MethodInfo method, object?[] arguments)
    {
        object? Invoke() => method.Invoke(instance, arguments);
        
        var attributes = method.GetCustomAttributes();
        
        foreach (var attribute in attributes)
        {
            switch (attribute)
            {
                case IMethodGuard normalGuard when !normalGuard.IsSatisfied(arguments):
                    return new GuardResult(false);
                case ISpecificMethodGuard specificGuard:
                {
                    if (GetParameterForGuard(specificGuard, arguments, out var param))
                    {
                        if (!specificGuard.IsSatisfied(param))
                        {
                            return new GuardResult(false);
                        }
                    }

                    break;
                }
            }
        }
        
        return new GuardResult(true, Invoke());
    }
    
    /// <summary>
    /// Invokes the given method with the given arguments, if the marked guards where satisfied.
    /// </summary>
    /// <param name="method">The method to be called.</param>
    /// <param name="arguments">The arguments of the method.</param>
    /// <returns>The <see cref="GuardResult"/> of the invocation.</returns>
    public GuardResult InvokeGuarded(MethodInfo method, object?[] arguments)
    {
        return InvokeGuarded(null, method, arguments);
    }
    
    private bool GetParameterForGuard(ISpecificMethodGuard guard, object?[] arguments, out object? param)
    {
        param = null;
        
        var spec = guard.GetType().GetCustomAttribute<ParameterSpecification>();
        if (spec == null)
        {
            return false;
        }
        
        var typeFiltered = arguments.Where(a => a != null && spec.Type.IsInstanceOfType(a)).ToList();

        if (!typeFiltered.Any())
        {
            return false;
        } 
        
        if (spec.Index.HasValue && spec.Index != -1)
        {
            param = typeFiltered.ElementAtOrDefault(spec.Index.Value);
            return true;
        }

        param = typeFiltered.FirstOrDefault();
        return true;
    }
}
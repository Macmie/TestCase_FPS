using UnityEngine;

public class RequireInterfaceAttribute : PropertyAttribute
{
    public System.Type requiredType { get; private set; }
    /// <summary>
    /// Requiring implementation of the requiredType interface.
    /// </summary>
    /// <param name="type">Interface type.</param>
    public RequireInterfaceAttribute(System.Type type)
    {
        this.requiredType = type;
    }
}


using System;
using UnityEngine;

namespace MiddleMast.Attributes
{
    /// <summary>
    /// Attribute that must be applied to a bool field to enable drawing of <see cref="MethodButtonAttribute"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class MethodButtonInvokerAttribute : PropertyAttribute { }
}

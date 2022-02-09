using System;

namespace MiddleMast.Attributes
{
    public enum InvokeTypes
    {
        Both,
        EditorOnly,
        PlayMode,
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class MethodButtonAttribute : Attribute
    {
        public MethodButtonAttribute(InvokeTypes invokeType = InvokeTypes.Both)
        {
            InvokeType = invokeType;
        }

        public InvokeTypes InvokeType { get; private set; }
    }
}

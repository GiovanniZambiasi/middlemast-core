using System;
using UnityEngine;

public class LabelledArrayAttribute : PropertyAttribute
{
    public readonly string[] Names;

    public LabelledArrayAttribute(string[] names) { this.Names = names; }

    public LabelledArrayAttribute(Type enumType) { Names = Enum.GetNames(enumType); }
}

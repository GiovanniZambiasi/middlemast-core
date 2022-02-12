using UnityEditor;

namespace MiddleMast.Editor
{
    public static class SerializedPropertyExtensions
    {
        public static bool ContainsArrayElement(this SerializedProperty property, UnityEngine.Object @object)
        {
            if (!property.isArray)
            {
                return false;
            }

            for (int i = 0; i < property.arraySize; i++)
            {
                SerializedProperty element = property.GetArrayElementAtIndex(i);

                if (element.objectReferenceValue == @object)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LabelledArrayAttribute))]
public class LabelledArrayDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, true);
    }

    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(rect, label, property);

        try
        {
            string path = property.propertyPath;
            int pos = (int)char.GetNumericValue(path[path.LastIndexOf('[') + 1]);
            EditorGUI.PropertyField(rect, property, new GUIContent(((LabelledArrayAttribute)attribute).Names[pos]), true);
        }
        catch
        {
            EditorGUI.PropertyField(rect, property, label, true);
        }

        EditorGUI.EndProperty();
    }
}

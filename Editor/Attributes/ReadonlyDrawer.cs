using MiddleMast.Attributes;
using UnityEditor;
using UnityEngine;

namespace MiddleMast.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadonlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property,label);
            GUI.enabled = true;
        }
    }
}

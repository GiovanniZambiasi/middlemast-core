using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MiddleMast.Attributes
{
    [CustomPropertyDrawer(typeof(MethodButtonInvokerAttribute), true)]
    public class MethodButtonInvokerDrawer : PropertyDrawer
    {
        private bool _shouldDraw;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Boolean)
            {
                EditorGUILayout.HelpBox("The <color=blue>MethodButtonInvoker</color> attribute must be assigned to a boolean value!", MessageType.Warning);
                _shouldDraw = false;

                return;
            }

            EditorGUI.PropertyField(position, property, label);

            if (_shouldDraw)
            {
                DrawButtons(property);
            }

            if (Event.current.type == EventType.Repaint)
            {
                _shouldDraw = property.boolValue;
            }
        }

        private static void DrawButtons(SerializedProperty property)
        {
            Object owner = property.serializedObject.targetObject;

            MethodInfo[] methods = owner.GetType()
                                        .GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            for (int i = 0; i < methods.Length; i++)
            {
                MethodInfo method = methods[i];
                MethodButtonAttribute attribute = method.GetAttribute<MethodButtonAttribute>();

                if (attribute == null)
                {
                    continue;
                }

                bool canPress = true;

                switch (attribute.InvokeType)
                {
                    case InvokeTypes.EditorOnly:
                        canPress = !Application.isPlaying;

                        break;

                    case InvokeTypes.PlayMode:
                        canPress = Application.isPlaying;

                        break;
                }

                GUI.enabled = canPress;

                if (GUILayout.Button(method.Name))
                {
                    method.Invoke(owner, new object[]
                                        { });
                }

                if (!canPress)
                {
                    GUI.enabled = true;
                }
            }
        }
    }
}

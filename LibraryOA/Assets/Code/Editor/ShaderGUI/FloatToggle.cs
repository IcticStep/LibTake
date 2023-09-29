using UnityEditor;
using UnityEngine;

namespace Code.Editor.ShaderGUI
{
    public static class FloatToggle
    {
        public static void DrawFloatToggle(GUIContent styles, MaterialProperty property, int indentLevel = 0, bool isDisabled = false)
        {
            if (property == null)
                return;

            EditorGUI.BeginDisabledGroup(isDisabled);
            EditorGUI.indentLevel += indentLevel;
            EditorGUI.BeginChangeCheck();
            MaterialEditor.BeginProperty(property);
            var newValue = EditorGUILayout.Toggle(styles, property.floatValue > 0.5f);
            if (EditorGUI.EndChangeCheck())
                property.floatValue = newValue ? 1.0f : 0.0f;
            MaterialEditor.EndProperty();
            EditorGUI.indentLevel -= indentLevel;
            EditorGUI.EndDisabledGroup();
        }
    }
}
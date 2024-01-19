using Code.Runtime.StaticData.GlobalGoals;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(SkillConstraint))]
    public class SkillConstraintDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Start the horizontal group
            EditorGUILayout.BeginHorizontal();

            // Get the SerializedProperties for _bookType and _requiredLevel
            SerializedProperty bookTypeProperty = property.FindPropertyRelative("_bookType");
            SerializedProperty requiredLevelProperty = property.FindPropertyRelative("_requiredLevel");

            // Calculate half width
            float halfWidth = position.width / 2;

            // Draw the BookType field
            Rect bookTypeRect = new Rect(position.x, position.y, halfWidth, position.height);
            EditorGUI.PropertyField(bookTypeRect, bookTypeProperty, GUIContent.none);

            // Draw the RequiredLevel field
            Rect requiredLevelRect = new Rect(position.x + halfWidth, position.y, halfWidth, position.height);
            EditorGUI.PropertyField(requiredLevelRect, requiredLevelProperty, GUIContent.none);

            // End the horizontal group
            EditorGUILayout.EndHorizontal();

            EditorGUI.EndProperty();
        }
    }
}
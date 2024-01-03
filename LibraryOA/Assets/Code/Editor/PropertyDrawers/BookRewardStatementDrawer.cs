using Code.Runtime.StaticData.Balance;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(BookRewardStatement))]
    public class BookRewardStatementDrawer : PropertyDrawer
    {
        private const int LabelWidth = 60;
        private const int SpaceBetweenLabelAndField = 10;
        private const int AdditionalSpaceBetweenSections = 20;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            float fieldWidth = CalculateFieldWidth(position.width);

            DrawLabelAndField(position, 0, "Progress >=", "_percentsLowerBound", fieldWidth, property);
            DrawLabelAndField(position, 1, "Reward:", "_reward", fieldWidth, property);

            EditorGUI.EndProperty();
        }

        private float CalculateFieldWidth(float totalWidth) =>
            (totalWidth - LabelWidth * 2 - SpaceBetweenLabelAndField * 2 - AdditionalSpaceBetweenSections) / 2;

        private void DrawLabelAndField(Rect position, int index, string labelText, string fieldName, float fieldWidth, SerializedProperty property)
        {
            float xOffset = index * (LabelWidth + fieldWidth + SpaceBetweenLabelAndField + AdditionalSpaceBetweenSections);
            Rect labelRect = new Rect(position.x + xOffset, position.y, LabelWidth, position.height);
            Rect fieldRect = new Rect(labelRect.x + LabelWidth + SpaceBetweenLabelAndField, position.y, fieldWidth, position.height);

            EditorGUI.LabelField(labelRect, labelText);
            EditorGUI.PropertyField(fieldRect, property.FindPropertyRelative(fieldName), GUIContent.none);
        }
    }
}
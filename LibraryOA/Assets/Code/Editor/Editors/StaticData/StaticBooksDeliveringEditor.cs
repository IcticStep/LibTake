using Code.Runtime.StaticData.Balance;
using UnityEditor;

namespace Code.Editor.Editors.StaticData
{
    [CustomEditor(typeof(StaticBooksDelivering))]
    internal sealed class StaticBooksDeliveringEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            StaticBooksDelivering bookReceiving = target as StaticBooksDelivering;
            DrawBookValuesForEachDay(bookReceiving);
        }

        private static void DrawBookValuesForEachDay(StaticBooksDelivering bookDelivering)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            for(int i = 1; i <= bookDelivering.DaysScale; i++)
            {
                EditorGUILayout.LabelField($"{i}) {bookDelivering.GetBooksShouldBeInLibraryForDay(i)} books");
            }
            EditorGUILayout.EndVertical();
        }
    }
}
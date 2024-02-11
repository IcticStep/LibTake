using System.Linq;
using System.Reflection;
using Code.Runtime.StaticData;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.StaticData
{
    [CustomEditor(typeof(ScenesRouting))]
    internal sealed class ScenesRoutingEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            ScenesRouting settings = (ScenesRouting)target;
            string[] scenes = GetAvailableScenes();

            // Using reflection to get all string fields from ScenesRouting
            var stringFields = typeof(ScenesRouting).GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Where(field => field.FieldType == typeof(string));

            foreach (var field in stringFields)
            {
                DrawSceneSelection(field, scenes, settings);
            }
        }

        private void DrawSceneSelection(FieldInfo field, string[] scenes, ScenesRouting settings)
        {
            EditorGUI.BeginChangeCheck();

            // Get current value of the field
            string currentScene = (string)field.GetValue(settings);
            string newScene = DrawScenePopup(ObjectNames.NicifyVariableName(field.Name), scenes, currentScene);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, $"Change {field.Name}");
                field.SetValue(settings, newScene); // Set new value to the field
                EditorUtility.SetDirty(target);
            }
        }

        private static string DrawScenePopup(string label, string[] scenes, string currentScene)
        {
            int currentIndex = GetCurrentSceneIndex(scenes, currentScene);
            return scenes[EditorGUILayout.Popup(label, currentIndex, scenes)];
        }

        private static int GetCurrentSceneIndex(string[] scenes, string sceneName)
        {
            int index = System.Array.IndexOf(scenes, sceneName);
            return index != -1 ? index : 0;
        }

        private static string[] GetAvailableScenes() =>
            EditorBuildSettings.scenes
                .Where(scene => scene.enabled)
                .Select(scene => System.IO.Path.GetFileNameWithoutExtension(scene.path))
                .ToArray();
    }
}

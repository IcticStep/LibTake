using System.Linq;
using Code.Runtime.StaticData;
using UnityEditor;

namespace Code.Editor.Editors.StaticData
{
    [CustomEditor(typeof(ScenesRouting))]
    internal sealed class StartupSettingsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            ScenesRouting settings = (ScenesRouting)target;
            string[] scenes = GetAvailableScenes();

            DrawSceneSelection("Bootstrap scene", scenes, ref settings.BootstrapScene);
            DrawSceneSelection("Menu scene", scenes, ref settings.MenuScene);
            DrawSceneSelection("Level scene", scenes, ref settings.LevelScene);
            DrawSceneSelection("Game over scene", scenes, ref settings.GameOverScene);
        }

        private void DrawSceneSelection(string label, string[] scenes, ref string selectedScene)
        {
            EditorGUI.BeginChangeCheck();
            string newScene = DrawScenePopup(label, scenes, selectedScene);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, $"Change {label}");
                selectedScene = newScene;
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
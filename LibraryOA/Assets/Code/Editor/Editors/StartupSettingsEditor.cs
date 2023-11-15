using System.Linq;
using Code.Runtime.StaticData;
using UnityEditor;

namespace Code.Editor.Editors
{
    [CustomEditor(typeof(StartupSettings))]
    internal sealed class StartupSettingsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            StartupSettings settings = (StartupSettings)target;

            string[] scenes = GetAvailableScenes();
            int currentSceneIndex = GetCurrentSelectedIndex(scenes, settings);
            int selectedSceneIndex = ShowScenePopup(currentSceneIndex, scenes);
            
            SaveIfChanged(selectedSceneIndex, currentSceneIndex, settings, scenes);
        }

        private static string[] GetAvailableScenes() =>
            EditorBuildSettings.scenes
                .Where(scene => scene.enabled)
                .Select(scene => System.IO.Path.GetFileNameWithoutExtension(scene.path))
                .ToArray();

        private static int GetCurrentSelectedIndex(string[] scenes, StartupSettings settings)
        {
            int currentSceneIndex = System.Array.IndexOf(scenes, settings.StartScene);
            if(currentSceneIndex == -1) currentSceneIndex = 0;
            return currentSceneIndex;
        }

        private static int ShowScenePopup(int currentSceneIndex, string[] scenes) =>
            EditorGUILayout.Popup("Start Scene", currentSceneIndex, scenes);

        private static void SaveIfChanged(int selectedSceneIndex, int currentSceneIndex, StartupSettings settings, string[] scenes)
        {
            if(selectedSceneIndex != currentSceneIndex)
            {
                Undo.RecordObject(settings, "Change Start Scene");
                settings.StartScene = scenes[selectedSceneIndex];
                EditorUtility.SetDirty(settings);
            }
        }
    }
}
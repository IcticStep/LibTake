using Code.Editor.Editors;
using Code.Editor.Editors.StaticData;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.StaticData;
using Code.Runtime.StaticData.Level;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Code.Editor.App
{
    [InitializeOnLoad]
    public class StaticDataAutoCollect
    {
        static StaticDataAutoCollect()
        {
            EditorSceneManager.sceneSaved += OnSceneSaved;
        }

        ~StaticDataAutoCollect()
        {
            EditorSceneManager.sceneSaved -= OnSceneSaved;
        }

        private static void OnSceneSaved(Scene scene)
        {
            IStaticDataService staticData = LoadLevelsData();
            LevelStaticData levelData = staticData.CurrentLevelData;
            if(levelData is null)
                return;
            
            LevelStaticDataEditor.UpdateLevelData(levelData);
        }

        private static IStaticDataService LoadLevelsData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadLevels();
            return staticData;
        }
    }
}
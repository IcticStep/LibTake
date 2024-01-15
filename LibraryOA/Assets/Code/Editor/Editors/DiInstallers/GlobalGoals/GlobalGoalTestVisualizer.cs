using System.Linq;
using Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals;
using UnityEditor;

namespace Code.Editor.Editors.DiInstallers.GlobalGoals
{
    public class GlobalGoalTestVisualizer
    {
        private int _testVisualizationGoalIndex;
        private bool _testVisualizationToggle;

        public void DrawTestVisualizationUi(GlobalGoalsInstaller globalGoalsInstaller)
        {
            using EditorGUILayout.ToggleGroupScope scope = new("Test Visualization", _testVisualizationToggle);
            _testVisualizationToggle = scope.enabled;
            
            if(!_testVisualizationToggle)
                return;
            
            string[] globalGoalsNames = GetGlobalGoalsNamesFromSchemes(globalGoalsInstaller);
            _testVisualizationGoalIndex = EditorGUILayout.Popup(_testVisualizationGoalIndex, globalGoalsNames);
        }

        private string[] GetGlobalGoalsNamesFromSchemes(GlobalGoalsInstaller globalGoalsInstaller) =>
            globalGoalsInstaller
                .GlobalGoalsVisualizationSchemes
                .Select(scheme => scheme.Goal.Name)
                .OrderBy(goalName => goalName)
                .ToArray();
    }
}
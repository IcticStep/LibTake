using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals;
using Code.Runtime.Logic.GlobalProgress;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.DiInstallers.GlobalGoals
{
    [CustomEditor(typeof(GlobalGoalsInstaller))]
    public class GlobalGoalsInstallerEditor : UnityEditor.Editor
    {
        private readonly GlobalGoalTestVisualizer _globalGoalTestVisualizer = new();
        
        private GlobalGoalsInstaller GlobalGoalsInstaller => (GlobalGoalsInstaller)target;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if(GUILayout.Button("Collect global goals data"))
                GlobalGoalsInstaller.GlobalGoalsVisualizationSchemes = CollectGlobalGoalsSchemes();

            _globalGoalTestVisualizer.DrawTestVisualizationUi(GlobalGoalsInstaller);
        }

        private static List<GlobalGoalVisualizationScheme> CollectGlobalGoalsSchemes() =>
            FindObjectsByType<GlobalStepPartVisualizer>(FindObjectsSortMode.None)
                .GroupBy(visualizer => visualizer.GlobalGoal)
                .Select(visualizerGroup => new GlobalGoalVisualizationScheme(visualizerGroup.Key, visualizerGroup.ToList()))
                .ToList();
    }
}
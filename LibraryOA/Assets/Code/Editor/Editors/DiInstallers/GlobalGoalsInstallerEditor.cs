using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals;
using Code.Runtime.Logic.GlobalProgress;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.DiInstallers
{
    [CustomEditor(typeof(GlobalGoalsInstaller))]
    public class GlobalGoalsInstallerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if(GUILayout.Button("Collect global goals data"))
                CollectGlobalGoalsData();
        }

        private void CollectGlobalGoalsData()
        {
            GlobalGoalsInstaller globalGoalsInstaller = (GlobalGoalsInstaller)target;
            globalGoalsInstaller.GlobalGoalsVisualizationSchemes = CollectGlobalGoalsSchemes();
        }

        private static List<GlobalGoalVisualizationScheme> CollectGlobalGoalsSchemes() =>
            FindObjectsByType<GlobalStepPartVisualizer>(FindObjectsSortMode.None)
                .GroupBy(visualizer => visualizer.GlobalGoal)
                .Select(visualizerGroup => new GlobalGoalVisualizationScheme(visualizerGroup.Key, visualizerGroup.ToList()))
                .ToList();
    }
}
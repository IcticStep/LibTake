using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals;
using Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals.Data;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.GlobalGoals;
using Code.Runtime.StaticData.GlobalGoals;
using UnityEditor;
using UnityEngine;

namespace Code.Editor.Editors.DiInstallers.GlobalGoals
{
    [CustomEditor(typeof(GlobalGoalsInstaller))]
    public class GlobalGoalsInstallerEditor : UnityEditor.Editor
    {
        private readonly GlobalGoalEditorVisualizer _globalGoalEditorVisualizer = new();
        private readonly StaticDataService _staticDataService = new();
        
        private GlobalGoalsInstaller GlobalGoalsInstaller => (GlobalGoalsInstaller)target;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button("Collect global goals data"))
            {
                _globalGoalEditorVisualizer.UpdateData(GlobalGoalsInstaller);
                GlobalGoalsInstaller.GlobalGoalsVisualizationSchemes = CollectGlobalGoalsSchemes();
            }

            _globalGoalEditorVisualizer.DrawTestVisualizationUi(GlobalGoalsInstaller);
        }

        private List<GlobalGoalScheme> CollectGlobalGoalsSchemes()
        {
            _staticDataService.LoadGlobalGoals();
            GlobalStepPartVisualizer[] visualizers = FindObjectsByType<GlobalStepPartVisualizer>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            return _staticDataService
                .GlobalGoals
                .Select(globalGoal => CreateGlobalGoalScheme(globalGoal, visualizers))
                .ToList();
        }

        private static GlobalGoalScheme CreateGlobalGoalScheme(GlobalGoal globalGoal, GlobalStepPartVisualizer[] visualizers) =>
            new(globalGoal, CreateGlobalStepSchemes(globalGoal, visualizers));

        private static List<GlobalStepScheme> CreateGlobalStepSchemes(GlobalGoal globalGoal, GlobalStepPartVisualizer[] visualizers) =>
            globalGoal
                .GlobalSteps
                .Select(globalStep => new GlobalStepScheme(
                    globalStep: globalStep,
                    visualizers: GetGlobalStepVisualizers(visualizers, globalStep),
                    cameraTarget: GetCameraTarget(visualizers, globalGoal, globalStep)))
                .ToList();

        private static GlobalStepPartVisualizer GetCameraTarget(GlobalStepPartVisualizer[] visualizers, GlobalGoal globalGoal, GlobalStep globalStep) =>
            visualizers
                .FirstOrDefault(visualizer => 
                    visualizer.GlobalGoal == globalGoal
                    && visualizer.GlobalStep == globalStep
                    && visualizer.IsCameraTarget
                    && visualizer.TargetStateAfterStep);

        private static List<GlobalStepPartVisualizer> GetGlobalStepVisualizers(GlobalStepPartVisualizer[] visualizers, GlobalStep globalStep) =>
            visualizers
                .Where(visualizer => visualizer.GlobalStep == globalStep)
                .ToList();
    }
}
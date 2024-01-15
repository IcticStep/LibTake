using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals;
using Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals.Data;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.StaticData.GlobalGoals;
using UnityEditor;

namespace Code.Editor.Editors.DiInstallers.GlobalGoals
{
    public class GlobalGoalTestVisualizer
    {
        private int _testVisualizationGoalIndex;
        private bool _testVisualizationToggle;
        private int _selectedVisualizationStep;
        private Dictionary<string, GlobalGoal> _globalGoalsByNames;
        private string[] _globalGoalsNames;

        public void UpdateData()
        {
            _globalGoalsByNames = GetGlobalGoalsByNames();
            _globalGoalsNames = _globalGoalsByNames
                .Values
                .Select(goal => goal.Name)
                .ToArray();
        }

        public void DrawTestVisualizationUi(GlobalGoalsInstaller globalGoalsInstaller)
        {
            using EditorGUILayout.ToggleGroupScope scope = new("Test Visualization", _testVisualizationToggle);
            _testVisualizationToggle = scope.enabled;
            
            if(!_testVisualizationToggle)
                return;
            
            InitDataIfNone();
            DrawGlobalGoalPopup();
            DrawVisualizationSlider(globalGoalsInstaller);
        }

        private void InitDataIfNone()
        {
            if(_globalGoalsNames is null)
                UpdateData();
        }

        private void DrawGlobalGoalPopup() =>
            _testVisualizationGoalIndex = EditorGUILayout.Popup(_testVisualizationGoalIndex, _globalGoalsNames);

        private void DrawVisualizationSlider(GlobalGoalsInstaller globalGoalsInstaller)
        {
            GlobalGoal selectedGoal = GetSelectedGoal();
            GlobalGoalScheme selectedGoalScheme = GetSelectedGoalScheme(globalGoalsInstaller, selectedGoal);
            int stepsCount = selectedGoalScheme.GlobalStepsSchemes.Count;
            _selectedVisualizationStep = EditorGUILayout.IntSlider(_selectedVisualizationStep, 0, stepsCount);
        }

        private GlobalGoal GetSelectedGoal()
        {
            string selectedName = _globalGoalsNames[_testVisualizationGoalIndex];
            GlobalGoal selectedGoal = _globalGoalsByNames[selectedName];
            return selectedGoal;
        }

        private static GlobalGoalScheme GetSelectedGoalScheme(GlobalGoalsInstaller globalGoalsInstaller, GlobalGoal selectedGoal) =>
            globalGoalsInstaller
                .GlobalGoalsVisualizationSchemes
                .First(scheme => scheme.Goal == selectedGoal);

        private Dictionary<string, GlobalGoal> GetGlobalGoalsByNames()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadGlobalGoals();
            
            return staticDataService
                .GlobalGoals
                .ToDictionary(
                    goal => goal.Name, 
                    goal => goal);
        }
    }
}
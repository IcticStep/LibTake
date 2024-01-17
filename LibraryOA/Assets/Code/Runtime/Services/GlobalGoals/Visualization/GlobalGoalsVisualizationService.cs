using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals.Data;
using Code.Runtime.Logic.GlobalGoals;
using Code.Runtime.StaticData.GlobalGoals;
using JetBrains.Annotations;

namespace Code.Runtime.Services.GlobalGoals.Visualization
{
    [UsedImplicitly]
    public sealed class GlobalGoalsVisualizationService : IGlobalGoalsVisualizationService
    {
        private Dictionary<GlobalGoal, GlobalGoalScheme> _goalSchemes;
        private Dictionary<GlobalStep, GlobalStepScheme> _currentGoalStepsSchemes;
        private GlobalGoal _currentGoal;

        private GlobalGoalScheme CurrentGoalScheme => _goalSchemes[_currentGoal];
        public bool InitializedGlobalGoal => _currentGoal is not null;

        public void InitializeVisualisationSchemes(IReadOnlyList<GlobalGoalScheme> allSchemes)
        {
            _goalSchemes = allSchemes.ToDictionary(scheme => scheme.Goal, scheme => scheme);
            _currentGoalStepsSchemes = null;
        }

        public void InitializeGlobalGoal(GlobalGoal globalGoal) =>
            _currentGoal = globalGoal;

        public void VisualizeStep(GlobalStep step)
        {
            GlobalStepScheme stepScheme = GetCurrentGoalStepScheme(step);
            VisualizeStepScheme(stepScheme);
        }

        /// <summary>
        /// Visualize by zero-based index respectively to <see cref="GlobalStep"/> list order in <see cref="GlobalGoal"/>> scriptable object. 
        /// </summary>
        /// <param name="index"></param>
        public void VisualizeStep(int index)
        {
            GlobalStep step = _currentGoal.GlobalSteps[index];
            GlobalStepScheme stepScheme = GetCurrentGoalStepScheme(step);
            VisualizeStepScheme(stepScheme);
        }

        public void VisualizeStepAndAllBefore(int index)
        {
            GlobalStep step = _currentGoal.GlobalSteps[index];
            VisualizeStepAndAllBefore(step);
        }
        
        public void VisualizeStepAndAllBefore(GlobalStep step)
        {
            foreach(GlobalStepScheme stepScheme in CurrentGoalScheme.GlobalStepsSchemes)
            {
                VisualizeStepScheme(stepScheme);
                if(stepScheme.Step == step)
                    return;
            }
        }

        public void ResetCurrentVisualization() =>
            ResetVisualization(CurrentGoalScheme);

        public void ResetAllVisualizations()
        {
            foreach(GlobalGoalScheme goalScheme in _goalSchemes.Values)
                ResetVisualization(goalScheme);                
        }

        private static void VisualizeStepScheme(GlobalStepScheme stepScheme)
        {
            foreach(GlobalStepPartVisualizer visualizer in stepScheme.Visualizers)
                visualizer.Visualize();
        }

        private static void ResetVisualization(GlobalGoalScheme goalScheme)
        {
            foreach(GlobalStepScheme globalStepsScheme in goalScheme.GlobalStepsSchemes)
                foreach(GlobalStepPartVisualizer visualizer in globalStepsScheme.Visualizers)
                    visualizer.Reset();
        }

        private GlobalStepScheme GetCurrentGoalStepScheme(GlobalStep globalStep)
        {
            InitializeCurrentGoalStepsSchemesIfNone();
            return _currentGoalStepsSchemes[globalStep];
        }

        private void InitializeCurrentGoalStepsSchemesIfNone() =>
            _currentGoalStepsSchemes ??= CurrentGoalScheme
                .GlobalStepsSchemes
                .ToDictionary(stepScheme => stepScheme.Step, stepScheme => stepScheme);
    }
}
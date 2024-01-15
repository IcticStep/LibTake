using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals.Data;
using Code.Runtime.Logic.GlobalProgress;
using Code.Runtime.StaticData.GlobalGoals;
using JetBrains.Annotations;

namespace Code.Runtime.Services.GlobalGoals.Visualization
{
    [UsedImplicitly]
    internal sealed class GlobalGoalsVisualizationService : IGlobalGoalsVisualizationService
    {
        private Dictionary<GlobalStep, GlobalStepScheme> _visualizers;

        public bool Initialized => _visualizers is not null;
        
        public void SetVisualisationScheme(GlobalGoalScheme goalScheme) =>
            _visualizers = goalScheme
                .GlobalStepsSchemes
                .ToDictionary(
                    stepScheme => stepScheme.Step,
                    stepScheme => stepScheme);

        public void VisualizeStep(GlobalStep step)
        {
            GlobalStepScheme stepScheme = _visualizers[step];
            foreach(GlobalStepPartVisualizer visualizer in stepScheme.Visualizers)
                visualizer.Visualize();
        }
    }
}
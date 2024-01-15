using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals;
using Code.Runtime.Logic.GlobalProgress;
using Code.Runtime.StaticData.GlobalGoals;
using JetBrains.Annotations;

namespace Code.Runtime.Services.GlobalGoals.Visualization
{
    [UsedImplicitly]
    internal sealed class GlobalGoalsVisualizationService : IGlobalGoalsVisualizationService
    {
        private Dictionary<GlobalStep, List<GlobalStepPartVisualizer>> _visualizers;

        public bool Initialized => _visualizers is not null;
        
        public void SetVisualisationScheme(GlobalGoalVisualizationScheme visualizationScheme) =>
            _visualizers = visualizationScheme
                .GlobalPartsSwitchers
                .GroupBy(switcher => switcher.Step)
                .ToDictionary(group => group.Key, group => group.ToList());

        public void VisualizeStep(GlobalStep step)
        {
            List<GlobalStepPartVisualizer> visualizers = _visualizers[step];
            foreach(GlobalStepPartVisualizer visualizer in visualizers)
                visualizer.Visualize();
        }
    }
}
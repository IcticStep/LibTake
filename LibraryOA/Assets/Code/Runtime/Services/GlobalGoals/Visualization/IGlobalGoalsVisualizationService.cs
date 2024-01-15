using Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals;
using Code.Runtime.StaticData.GlobalGoals;

namespace Code.Runtime.Services.GlobalGoals.Visualization
{
    internal interface IGlobalGoalsVisualizationService
    {
        bool Initialized { get; }
        void SetVisualisationScheme(GlobalGoalVisualizationScheme visualizationScheme);
        void VisualizeStep(GlobalStep step);
    }
}
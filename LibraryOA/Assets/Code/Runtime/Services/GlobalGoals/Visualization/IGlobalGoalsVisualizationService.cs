using Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals;
using Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals.Data;
using Code.Runtime.StaticData.GlobalGoals;

namespace Code.Runtime.Services.GlobalGoals.Visualization
{
    internal interface IGlobalGoalsVisualizationService
    {
        bool Initialized { get; }
        void SetVisualisationScheme(GlobalGoalScheme goalScheme);
        void VisualizeStep(GlobalStep step);
    }
}
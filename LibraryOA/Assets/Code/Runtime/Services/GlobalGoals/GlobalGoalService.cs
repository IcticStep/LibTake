using Code.Runtime.Services.GlobalGoals.Visualization;
using Code.Runtime.Services.Interactions.Crafting;
using Code.Runtime.StaticData.GlobalGoals;
using JetBrains.Annotations;
using Zenject;

namespace Code.Runtime.Services.GlobalGoals
{
    [UsedImplicitly]
    internal sealed class GlobalGoalService : IGlobalGoalService
    {
        private ICraftingService _craftingService;
        private IGlobalGoalsVisualizationService _globalGoalsVisualizationService;
        private GlobalGoal _globalGoal;

        [Inject]
        private void Construct(ICraftingService craftingService, IGlobalGoalsVisualizationService globalGoalsVisualizationService)
        {
            _globalGoalsVisualizationService = globalGoalsVisualizationService;
            _craftingService = craftingService;
        }
        
        public void SetGlobalGoal(GlobalGoal globalGoal)
        {
            _globalGoal = globalGoal;
            _craftingService.SetGoal(_globalGoal);
            _globalGoalsVisualizationService.InitializeGlobalGoal(_globalGoal);
        }
    }
}
using System.Linq;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.StaticData;
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
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(ICraftingService craftingService, IGlobalGoalsVisualizationService globalGoalsVisualizationService, IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _globalGoalsVisualizationService = globalGoalsVisualizationService;
            _craftingService = craftingService;
        }
        
        public void SetGlobalGoal(GlobalGoal globalGoal)
        {
            _globalGoal = globalGoal;
            _craftingService.SetGoal(_globalGoal);
            _globalGoalsVisualizationService.InitializeGlobalGoal(_globalGoal);
        }

        public void LoadProgress(GameProgress progress)
        {
            GlobalGoalSavedData savedData = progress.PlayerData.GlobalGoal;
            if(savedData.GoalId is null)
                return;
            
            _globalGoal = _staticDataService.GlobalGoals.First(goal => goal.UniqueId == savedData.GoalId);
        }

        public void UpdateProgress(GameProgress progress) =>
            progress.PlayerData.GlobalGoal.GoalId = _globalGoal.UniqueId;
    }
}
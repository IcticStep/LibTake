using System;
using System.Linq;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.GlobalGoals.Presenter;
using Code.Runtime.Services.GlobalGoals.Visualization;
using Code.Runtime.Services.Player.Inventory;
using Code.Runtime.Services.Skills;
using Code.Runtime.StaticData.GlobalGoals;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Interactions.Crafting
{
    [UsedImplicitly]
    internal sealed class CraftingService : ICraftingService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IPlayerSkillService _playerSkillService;
        private readonly IPlayerInventoryService _playerInventoryService;
        private readonly IGlobalGoalPresenterService _presenterService;
        private readonly IGlobalGoalsVisualizationService _globalGoalsVisualizationService;

        private string _globalGoalId;
        
        public GlobalGoal Goal { get; private set; }
        public int CurrentStepIndex { get; private set; }
        public bool PayedForStep { get; private set; }
        
        public bool CraftingAllowed { get; private set; } = true;

        public GlobalStep CurrentStep => FinishedGoal ? Goal.GlobalSteps[^1] : Goal.GlobalSteps[CurrentStepIndex];
        public GlobalStep PreviousStep => CurrentStepIndex == 0 ? null : Goal.GlobalSteps[CurrentStepIndex - 1];
        public bool FinishedGoal => CurrentStepIndex == Goal.GlobalSteps.Count;
        
        public event Action<bool> CraftingPermissionChanged;

        public CraftingService(IStaticDataService staticDataService, IPlayerSkillService playerSkillService, IPlayerInventoryService playerInventoryService,
            IGlobalGoalPresenterService presenterService, IGlobalGoalsVisualizationService globalGoalsVisualizationService)
        {
            _staticDataService = staticDataService;
            _playerSkillService = playerSkillService;
            _playerInventoryService = playerInventoryService;
            _presenterService = presenterService;
            _globalGoalsVisualizationService = globalGoalsVisualizationService;
        }

        public void SetGoal(GlobalGoal globalGoal)
        {
            Goal = globalGoal;
            _globalGoalId = Goal.UniqueId;
        }
        
        public void AllowCrafting()
        {
            CraftingAllowed = true;
            CraftingPermissionChanged?.Invoke(CraftingAllowed);
        }
        
        public void BlockCrafting()
        {
            CraftingAllowed = false;
            CraftingPermissionChanged?.Invoke(CraftingAllowed);
        }

        public async UniTask CraftStep()
        {
            if(!CanCraftStep())
                throw new InvalidOperationException("Can't craft step!");

            _globalGoalsVisualizationService.VisualizeStep(CurrentStep);
            await _presenterService.ShowBuiltStep(CurrentStep, Goal);
            CurrentStepIndex++;
            PayedForStep = false;
        }

        public int PayForStep()
        {
            if(!CanPayForStep())
                return 0;

            _playerInventoryService.RemoveCoins(CurrentStep.Cost);
            PayedForStep = true;
            return CurrentStep.Cost;
        }

        public bool CanCraftStep()
        {
            if(FinishedGoal)
                return true;
            return PayedForStep && HaveEnoughSkillsToCraft();
        }

        public bool CanPayForStep() =>
            !FinishedGoal
            && !PayedForStep
            &&  _playerInventoryService.Coins >= CurrentStep.Cost;

        public bool HaveEnoughSkillsToCraft()
        {
            foreach(SkillConstraint skill in CurrentStep.SkillRequirements)
            {
                int currentLevel = _playerSkillService.GetSkillByBookType(skill.BookType);
                int neededLevel = skill.RequiredLevel;

                if(currentLevel < neededLevel)
                    return false;
            }

            return true;
        }

        public void LoadProgress(GameProgress progress)
        {
            GlobalGoalSavedData savedData = progress.PlayerData.GlobalGoal;
            if(savedData.GoalId is null)
                return;
            
            _globalGoalId = savedData.GoalId;
            CurrentStepIndex = savedData.GoalStepIndex;
            PayedForStep = savedData.PayedForStep;
            Goal = _staticDataService.GlobalGoals.First(goal => goal.UniqueId == _globalGoalId);
            _globalGoalsVisualizationService.InitializeGlobalGoal(Goal);
            
            if(PreviousStep is not null)
                _globalGoalsVisualizationService.VisualizeStepAndAllBefore(PreviousStep);
        }

        public void UpdateProgress(GameProgress progress)
        {
            progress.PlayerData.GlobalGoal.GoalStepIndex = CurrentStepIndex;
            progress.PlayerData.GlobalGoal.PayedForStep = PayedForStep;
        }

        public void CleanUp()
        {
            _globalGoalId = null;
            CurrentStepIndex = 0;
            PayedForStep = false;
        }
    }
}
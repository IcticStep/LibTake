using System;
using System.Linq;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.Player.Inventory;
using Code.Runtime.Services.Skills;
using Code.Runtime.StaticData.GlobalGoals;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Interactions.Crafting
{
    [UsedImplicitly]
    internal sealed class CraftingService : ICraftingService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ISkillService _skillService;
        private readonly IPlayerInventoryService _playerInventoryService;

        private string _globalGoalId;
        public GlobalGoal Goal { get; private set; }
        public int CurrentStepIndex { get; private set; }
        public bool PayedForStep { get; private set; }
        public GlobalStep CurrentStep => Goal.GlobalSteps[CurrentStepIndex];
        public bool FinishedGoal => CurrentStepIndex == Goal.GlobalSteps.Count - 1;

        public CraftingService(IStaticDataService staticDataService, ISkillService skillService, IPlayerInventoryService playerInventoryService)
        {
            _staticDataService = staticDataService;
            _skillService = skillService;
            _playerInventoryService = playerInventoryService;
        }

        public void SetGoal(GlobalGoal globalGoal)
        {
            Goal = globalGoal;
            _globalGoalId = Goal.UniqueId;
        }

        public void CraftStep()
        {
            if(!CanCraftStep())
                throw new InvalidOperationException("Can't craft step!");

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

        public bool CanCraftStep() =>
            PayedForStep && HaveEnoughSkillsToCraft();

        public bool CanPayForStep() =>
            !FinishedGoal
            && !PayedForStep
            &&  _playerInventoryService.Coins >= CurrentStep.Cost;

        public bool HaveEnoughSkillsToCraft()
        {
            foreach(SkillConstraint skill in CurrentStep.SkillRequirements)
            {
                int currentLevel = _skillService.GetSkillByBookType(skill.BookType);
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
        }

        public void UpdateProgress(GameProgress progress)
        {
            progress.PlayerData.GlobalGoal.GoalId = _globalGoalId;
            progress.PlayerData.GlobalGoal.GoalStepIndex = CurrentStepIndex;
            progress.PlayerData.GlobalGoal.PayedForStep = PayedForStep;
        }
    }
}
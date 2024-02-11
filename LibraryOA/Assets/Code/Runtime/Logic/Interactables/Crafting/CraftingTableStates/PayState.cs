using System;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates.Api;
using Code.Runtime.Services.Interactions.Crafting;
using Code.Runtime.StaticData.GlobalGoals;
using Code.Runtime.Ui.FlyingResources;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates
{
    internal sealed class PayState : ICraftingTableState, IRewardSource
    {
        private readonly CraftingTableStateMachine _craftingTableStateMachine;
        private readonly ICraftingService _craftingService;

        public int Price => _craftingService.FinishedGoal ? 0 : _craftingService.CurrentStep.Cost;
        public GlobalStep CurrentStep => _craftingService.CurrentStep;
        
        public event Action<int> Rewarded;

        public PayState(CraftingTableStateMachine craftingTableStateMachine, ICraftingService craftingService)
        {
            _craftingTableStateMachine = craftingTableStateMachine;
            _craftingService = craftingService;
        }

        public bool CanInteract() =>
            _craftingService.CanPayForStep();

        public void Interact()
        {
            if(!CanInteract())
                return;
                
            int payed = _craftingService.PayForStep();
            Rewarded?.Invoke(-payed);
            _craftingTableStateMachine.Enter<SkillCheckState>();
        }
    }
}
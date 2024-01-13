using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates.Api;
using Code.Runtime.Services.Interactions.Crafting;

namespace Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates
{
    internal sealed class PayState : ICraftingTableState
    {
        private readonly CraftingTableStateMachine _craftingTableStateMachine;
        private readonly ICraftingService _craftingService;

        public int Price => _craftingService.FinishedGoal ? 0 : _craftingService.CurrentStep.Cost; 

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
                
            _craftingService.PayForStep();
            _craftingTableStateMachine.Enter<PayState>();
        }
    }
}
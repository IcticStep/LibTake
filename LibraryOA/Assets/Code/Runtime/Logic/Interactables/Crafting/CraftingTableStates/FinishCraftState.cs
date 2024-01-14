using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates.Api;
using Code.Runtime.Services.Interactions.Crafting;

namespace Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates
{
    internal class FinishCraftState : ICraftingTableState
    {
        private readonly CraftingTableStateMachine _craftingTableStateMachine;
        private readonly ICraftingService _craftingService;

        public FinishCraftState(CraftingTableStateMachine craftingTableStateMachine, ICraftingService craftingService)
        {
            _craftingTableStateMachine = craftingTableStateMachine;
            _craftingService = craftingService;
        }

        public bool CanInteract() =>
            true;

        public void Interact()
        {
            _craftingService.CraftStep();
            _craftingTableStateMachine.Enter<PayState>();
        }
    }
}
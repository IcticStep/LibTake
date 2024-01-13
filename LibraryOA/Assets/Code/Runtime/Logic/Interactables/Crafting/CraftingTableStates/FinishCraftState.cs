using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates.Api;

namespace Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates
{
    internal class FinishCraftState : ICraftingTableState
    {
        private readonly CraftingTableStateMachine _craftingTableStateMachine;

        public FinishCraftState(CraftingTableStateMachine craftingTableStateMachine)
        {
            _craftingTableStateMachine = craftingTableStateMachine;
        }

        public bool CanInteract() =>
            true;

        public void Interact() =>
            throw new System.NotImplementedException();
    }
}
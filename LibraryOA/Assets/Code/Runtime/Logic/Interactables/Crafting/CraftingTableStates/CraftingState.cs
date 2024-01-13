using Code.Runtime.Logic.Interactables.Api;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates.Api;
using Code.Runtime.Services.Interactions.Crafting;

namespace Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates
{
    internal sealed class CraftingState : ICraftingTableState, IHoverStartListener, IHoverEndListener
    {
        private readonly CraftingTableStateMachine _craftingTableStateMachine;
        private readonly ICraftingService _craftingService;

        public CraftingState(CraftingTableStateMachine craftingTableStateMachine, ICraftingService craftingService)
        {
            _craftingTableStateMachine = craftingTableStateMachine;
            _craftingService = craftingService;
        }

        public bool CanInteract() =>
            throw new System.NotImplementedException();

        public void Interact() =>
            throw new System.NotImplementedException();

        public void OnHoverStart() =>
            throw new System.NotImplementedException();

        public void OnHoverEnd() =>
            throw new System.NotImplementedException();
    }
}
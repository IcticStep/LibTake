using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates.Api;
using Code.Runtime.Services.Interactions.Crafting;

namespace Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates
{
    internal sealed class SkillCheckState : ICraftingTableState
    {
        private readonly CraftingTableStateMachine _craftingTableStateMachine;
        private readonly ICraftingService _craftingService;

        public SkillCheckState(CraftingTableStateMachine craftingTableStateMachine, ICraftingService craftingService)
        {
            _craftingTableStateMachine = craftingTableStateMachine;
            _craftingService = craftingService;
        }

        public bool CanInteract() =>
            _craftingService.HaveEnoughSkillsToCraft();

        public void Interact() =>
            throw new System.NotImplementedException();
    }
}
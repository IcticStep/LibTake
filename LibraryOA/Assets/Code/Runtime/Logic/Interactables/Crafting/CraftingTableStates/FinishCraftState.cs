using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates.Api;
using Code.Runtime.Services.Interactions.Crafting;
using Code.Runtime.StaticData.Interactables;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates
{
    internal class FinishCraftState : ICraftingTableState
    {
        private readonly CraftingTableStateMachine _craftingTableStateMachine;
        private readonly ICraftingService _craftingService;
        private readonly IStaticDataService _staticDataService;
        
        private StaticCraftingTable CraftingTableData => _staticDataService.Interactables.CraftingTable;

        public FinishCraftState(CraftingTableStateMachine craftingTableStateMachine, ICraftingService craftingService, IStaticDataService staticDataService)
        {
            _craftingTableStateMachine = craftingTableStateMachine;
            _craftingService = craftingService;
            _staticDataService = staticDataService;
        }

        public bool CanInteract() =>
            true;

        public void Interact()
        {
            _craftingService.CraftStep();
            EnterPayStateDelayed().Forget();
        }

        private async UniTaskVoid EnterPayStateDelayed()
        {
            await UniTask.WaitForSeconds(CraftingTableData.PayStateEnterSecondsDelay);
            _craftingTableStateMachine.Enter<PayState>();
        }
    }
}
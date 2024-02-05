using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates.Api;
using Code.Runtime.Services.GlobalGoals.Finish;
using Code.Runtime.Services.Interactions.Crafting;
using Code.Runtime.StaticData.Interactables;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates
{
    internal class FinishCraftState : ICraftingTableState, IStartable
    {
        private readonly CraftingTableStateMachine _craftingTableStateMachine;
        private readonly ICraftingService _craftingService;
        private readonly IStaticDataService _staticDataService;
        private readonly IGlobalGoalFinishService _globalGoalFinishService;

        private bool _canInteract;
        
        private StaticCraftingTable CraftingTableData => _staticDataService.Interactables.CraftingTable;

        public FinishCraftState(CraftingTableStateMachine craftingTableStateMachine, ICraftingService craftingService, IStaticDataService staticDataService,
            IGlobalGoalFinishService globalGoalFinishService)
        {
            _craftingTableStateMachine = craftingTableStateMachine;
            _craftingService = craftingService;
            _staticDataService = staticDataService;
            _globalGoalFinishService = globalGoalFinishService;
        }

        public void Start() =>
            _canInteract = true;

        public bool CanInteract() =>
            _canInteract;

        public void Interact() =>
            ProcessInteraction()
                .Forget();

        private async UniTaskVoid ProcessInteraction()
        {
            _canInteract = false;
            await _craftingService.CraftStep();

            if(_craftingService.FinishedGoal)
                FinishGlobalGoal();
            else
                EnterPayStateDelayed().Forget();
        }

        private async UniTaskVoid EnterPayStateDelayed()
        {
            await UniTask.WaitForSeconds(CraftingTableData.PayStateEnterSecondsDelay);
            _craftingTableStateMachine.Enter<PayState>();
        }

        private void FinishGlobalGoal() =>
            _globalGoalFinishService.FinishGlobalGoal();
    }
}
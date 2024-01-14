using Code.Runtime.Logic.Interactables.Api;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates.Api;
using Code.Runtime.Services.Interactions.Crafting;

namespace Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates
{
    internal sealed class CraftingState : ICraftingTableState, IHoverStartListener, IHoverEndListener, IStartable
    {
        private readonly CraftingTableStateMachine _craftingTableStateMachine;
        private readonly ICraftingService _craftingService;
        private readonly IProgress _progress;

        public CraftingState(CraftingTableStateMachine craftingTableStateMachine, ICraftingService craftingService, IProgress progress)
        {
            _craftingTableStateMachine = craftingTableStateMachine;
            _craftingService = craftingService;
            _progress = progress;
        }

        public bool CanInteract() =>
            false;

        public void Interact() { }

        public void Start()
        {
            _progress.Initialize(timeToFinish: _craftingService.CurrentStep.Duration);
            _progress.StartFilling(OnCraftFinished);
        }

        public void OnHoverStart()
        {
            if(CanCraft(_progress))
                _progress.StartFilling(OnCraftFinished);
        }

        public void OnHoverEnd() =>
            _progress.StopFilling();

        private bool CanCraft(IProgress progress) =>
            progress.CanBeStarted 
            && _craftingService.CanCraftStep();

        private void OnCraftFinished()
        {
            _craftingTableStateMachine.Enter<FinishCraftState>();
            _progress.Reset();
            _craftingService.CraftStep();
        }
    }
}
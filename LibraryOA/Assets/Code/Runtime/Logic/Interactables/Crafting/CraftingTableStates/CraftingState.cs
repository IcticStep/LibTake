using Code.Runtime.Logic.Interactables.Api;
using Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates.Api;
using Code.Runtime.Services.Interactions.Crafting;
using Code.Runtime.Services.Player.Provider;

namespace Code.Runtime.Logic.Interactables.Crafting.CraftingTableStates
{
    internal sealed class CraftingState : ICraftingTableState, IHoverStartListener, IHoverEndListener, IStartable, IExitable
    {
        private readonly CraftingTableStateMachine _craftingTableStateMachine;
        private readonly ICraftingService _craftingService;
        private readonly IProgress _progress;
        private readonly IPlayerProviderService _playerProviderService;

        private bool InFocus => _playerProviderService.InteractablesScanner.CurrentFocusedInteractable == _craftingTableStateMachine;

        public CraftingState(CraftingTableStateMachine craftingTableStateMachine, ICraftingService craftingService, IProgress progress,
            IPlayerProviderService playerProviderService)
        {
            _craftingTableStateMachine = craftingTableStateMachine;
            _craftingService = craftingService;
            _progress = progress;
            _playerProviderService = playerProviderService;
        }

        public bool CanInteract() =>
            false;

        public void Interact() { }

        public void Start()
        {
            _craftingService.CraftingPermissionChanged += OnCraftingPermissionChanged;
            _progress.Initialize(timeToFinish: _craftingService.CurrentStep.Duration);
            
            if(InFocus)
                _progress.StartFilling(OnCraftFinished);
        }

        public void Exit() =>
            _craftingService.CraftingPermissionChanged -= OnCraftingPermissionChanged;

        public void OnHoverStart()
        {
            if(CanCraft(_progress))
                _progress.StartFilling(OnCraftFinished);
        }

        public void OnHoverEnd() =>
            _progress.StopFilling();

        private void OnCraftingPermissionChanged(bool newValue)
        {
            if(newValue == false)
                _progress.StopFilling();
        }

        private bool CanCraft(IProgress progress) =>
            progress.CanBeStarted 
            && _craftingService.CanCraftStep();

        private void OnCraftFinished()
        {
            _craftingTableStateMachine.Enter<FinishCraftState>();
            _progress.Reset();
        }
    }
}
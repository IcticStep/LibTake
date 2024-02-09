using Code.Runtime.Infrastructure.GameStates.Api;
using Code.Runtime.Infrastructure.Services.Camera;
using Code.Runtime.Infrastructure.Services.UiHud;
using Code.Runtime.Services.Customers.Registry;
using Code.Runtime.Services.GlobalGoals.Visualization;
using Code.Runtime.Services.InputService;
using Code.Runtime.Services.Library;

namespace Code.Runtime.Infrastructure.GameStates.States
{
    internal sealed class FinishGlobalGoalState : IGameState
    {
        private readonly IInputService _inputService;
        private readonly ICameraProvider _cameraProvider;
        private readonly IGlobalGoalsVisualizationService _globalGoalsVisualizationService;
        private readonly IHudProviderService _hudProviderService;
        private readonly ICustomersRegistryService _customersRegistryService;
        private readonly ILibraryService _libraryService;

        public FinishGlobalGoalState(IInputService inputService, ICameraProvider cameraProvider, IGlobalGoalsVisualizationService globalGoalsVisualizationService,
            IHudProviderService hudProviderService, ICustomersRegistryService customersRegistryService, ILibraryService libraryService)
        {
            _inputService = inputService;
            _cameraProvider = cameraProvider;
            _globalGoalsVisualizationService = globalGoalsVisualizationService;
            _hudProviderService = hudProviderService;
            _customersRegistryService = customersRegistryService;
            _libraryService = libraryService;
        }

        public void Start()
        {
            StopGameplay();

            _libraryService.ShowSecondFloor();
        }

        public void Exit() { }

        private void StopGameplay()
        {
            _cameraProvider.DisableFollow();
            _cameraProvider.EnableAnimator();
            _inputService.Disable();
            _hudProviderService.Hide();
            _customersRegistryService.ForceStopAllCustomers();
            _globalGoalsVisualizationService.PlayFinishCutscene();
        }
    }
}
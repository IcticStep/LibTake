
using Code.Runtime.Infrastructure.Services.Camera;
using Code.Runtime.Infrastructure.Services.UiHud;
using Code.Runtime.Services.Customers.Queue;
using Code.Runtime.Services.Customers.Registry;
using Code.Runtime.Services.GlobalGoals.Visualization;
using Code.Runtime.Services.InputService;
using JetBrains.Annotations;

namespace Code.Runtime.Services.GlobalGoals.Finish
{
    [UsedImplicitly]
    internal sealed class GlobalGoalFinishService : IGlobalGoalFinishService
    {
        private readonly IInputService _inputService;
        private readonly ICameraProvider _cameraProvider;
        private readonly IGlobalGoalsVisualizationService _globalGoalsVisualizationService;
        private readonly IHudProviderService _hudProviderService;
        private readonly ICustomersRegistryService _customersRegistryService;

        public GlobalGoalFinishService(IInputService inputService, ICameraProvider cameraProvider, IGlobalGoalsVisualizationService globalGoalsVisualizationService,
            IHudProviderService hudProviderService, ICustomersRegistryService customersRegistryService)
        {
            _inputService = inputService;
            _cameraProvider = cameraProvider;
            _globalGoalsVisualizationService = globalGoalsVisualizationService;
            _hudProviderService = hudProviderService;
            _customersRegistryService = customersRegistryService;
        }
        
        public void FinishGlobalGoal()
        {
            _cameraProvider.DisableFollow();
            _cameraProvider.EnableAnimator();
            _inputService.Disable();
            _hudProviderService.Hide();
            _customersRegistryService.
            _globalGoalsVisualizationService.PlayFinishCutscene();
        }
    }
}
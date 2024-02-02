
using Code.Runtime.Infrastructure.Services.Camera;
using Code.Runtime.Services.GlobalGoals.Visualization;
using JetBrains.Annotations;

namespace Code.Runtime.Services.GlobalGoals.Finish
{
    [UsedImplicitly]
    internal sealed class GlobalGoalFinishService : IGlobalGoalFinishService
    {
        private readonly InputService.InputService _inputService;
        private readonly ICameraProvider _cameraProvider;
        private readonly IGlobalGoalsVisualizationService _globalGoalsVisualizationService;

        public GlobalGoalFinishService(InputService.InputService inputService, ICameraProvider cameraProvider, IGlobalGoalsVisualizationService globalGoalsVisualizationService)
        {
            _inputService = inputService;
            _cameraProvider = cameraProvider;
            _globalGoalsVisualizationService = globalGoalsVisualizationService;
        }
        
        public void FinishGlobalGoal()
        {
            _cameraProvider.DisableFollow();
            _cameraProvider.EnableAnimator();
            _inputService.Disable();
            _globalGoalsVisualizationService.PlayFinishCutscene();
        }
    }
}
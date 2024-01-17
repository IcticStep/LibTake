using System.Linq;
using Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals.Data;
using Code.Runtime.Infrastructure.Services.Camera;
using Code.Runtime.Infrastructure.Services.HudProvider;
using Code.Runtime.Services.GlobalGoals.Visualization;
using Code.Runtime.Services.InputService;
using Code.Runtime.StaticData.GlobalGoals;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Services.GlobalGoals.Presenter
{
    [UsedImplicitly]
    internal sealed class GlobalGoalPresenterService : IGlobalGoalPresenterService
    {
        private IGlobalGoalsVisualizationService _globalGoalsVisualizationService;
        private IHudProviderService _hudProviderService;
        private IInputService _inputService;
        private ICameraProvider _cameraProvider;

        [Inject]
        private void Construct(IGlobalGoalsVisualizationService globalGoalsVisualizationService, IHudProviderService hudProviderService, IInputService inputService,
            ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
            _inputService = inputService;
            _hudProviderService = hudProviderService;
            _globalGoalsVisualizationService = globalGoalsVisualizationService;
        }

        public async UniTaskVoid ShowBuiltStep(GlobalStep globalStep)
        {
            GlobalStepScheme scheme = GetGlobalStepScheme(globalStep);
            _inputService.Disable();
            _hudProviderService.Hide();

            await ShowCameraAnimationAsync(scheme);
            
            _inputService.Enable();
            _hudProviderService.Show();
        }

        private async UniTask ShowCameraAnimationAsync(GlobalStepScheme scheme)
        {
            Transform oldCameraTarget = _cameraProvider.CameraFollow.Target;
            _cameraProvider.CameraFollow.SetTarget(scheme.CameraTarget.transform);
            await UniTask.WaitForSeconds(1f);
            _cameraProvider.CameraFollow.SetTarget(oldCameraTarget);
            await UniTask.WaitForSeconds(1f);
        }

        private GlobalStepScheme GetGlobalStepScheme(GlobalStep globalStep) =>
            _globalGoalsVisualizationService
                .CurrentGoalScheme
                .GlobalStepsSchemes
                .First(scheme => scheme.Step == globalStep);
    }
}
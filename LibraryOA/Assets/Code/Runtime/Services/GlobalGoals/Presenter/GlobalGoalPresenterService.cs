using System;
using System.Linq;
using Code.Runtime.Infrastructure.DiInstallers.Library.GlobalGoals.Data;
using Code.Runtime.Infrastructure.Services.Camera;
using Code.Runtime.Infrastructure.Services.UiHud;
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

        public event Action GlobalStepCompleted;

        [Inject]
        private void Construct(IGlobalGoalsVisualizationService globalGoalsVisualizationService, IHudProviderService hudProviderService, IInputService inputService,
            ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
            _inputService = inputService;
            _hudProviderService = hudProviderService;
            _globalGoalsVisualizationService = globalGoalsVisualizationService;
        }

        public async UniTask ShowBuiltStep(GlobalStep globalStep, GlobalGoal globalGoal)
        {
            GlobalStepScheme scheme = GetGlobalStepScheme(globalStep);
            _inputService.Disable();
            _hudProviderService.Hide();

            Transform oldCameraTarget = _cameraProvider.CameraFollow.Target;
            GlobalStepCompleted?.Invoke();
            await _cameraProvider.CameraFollow.MoveToNewTargetAsync(scheme.CameraTarget, globalGoal.CameraMoveDuration);
            await UniTask.WaitForSeconds(globalGoal.CameraLookAtStepCompletedDelay);
            ShowHudOnHalfCameraWayBack(globalGoal.CameraMoveDuration).Forget();
            await _cameraProvider.CameraFollow.MoveToNewTargetAsync(oldCameraTarget, globalGoal.CameraMoveDuration);

            _inputService.Enable();
        }

        private GlobalStepScheme GetGlobalStepScheme(GlobalStep globalStep) =>
            _globalGoalsVisualizationService
                .CurrentGoalScheme
                .GlobalStepsSchemes
                .First(scheme => scheme.Step == globalStep);

        private async UniTaskVoid ShowHudOnHalfCameraWayBack(float cameraReturningTime)
        {
            await UniTask.WaitForSeconds(cameraReturningTime / 2);
            _hudProviderService.Show();
        }
    }
}
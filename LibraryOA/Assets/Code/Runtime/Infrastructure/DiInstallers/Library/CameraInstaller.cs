using Code.Runtime.Infrastructure.Services.Camera;
using Code.Runtime.Logic.CameraControl;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Infrastructure.DiInstallers.Library
{
    internal sealed class CameraInstaller : MonoInstaller, IInitializable
    {
        [SerializeField]
        private CameraFollow _mainCamera;
        
        private ICameraProvider _cameraProvider;

        [Inject]
        private void Construct(ICameraProvider cameraProvider) =>
            _cameraProvider = cameraProvider;

        public override void InstallBindings() =>
            InitializeCameraProvider();

        public void Initialize() =>
            InitializeCameraProvider();
        
        private void InitializeCameraProvider() =>
            _cameraProvider.Initialize(_mainCamera);
    }
}
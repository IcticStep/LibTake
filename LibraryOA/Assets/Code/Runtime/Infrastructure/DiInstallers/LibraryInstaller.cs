using Code.Runtime.Infrastructure.Services.Camera;
using Code.Runtime.Logic.CameraControl;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Runtime.Infrastructure.DiInstallers
{
    internal sealed class LibraryInstaller : MonoInstaller, IInitializable
    {
        [SerializeField]
        private CameraFollow _mainCamera;
        
        private ICameraProvider _cameraProvider;

        [Inject]
        public void Construct(ICameraProvider cameraProvider) =>
            _cameraProvider = cameraProvider;

        public override void InstallBindings()
        {
            BindCamera();
            InitializeCameraProvider();
        }

        public void Initialize() =>
            InitializeCameraProvider();

        private void BindCamera() =>
            Container
                .Bind<ICameraFollow>()
                .To<CameraFollow>()
                .FromInstance(_mainCamera)
                .AsSingle();
        
        private void InitializeCameraProvider() =>
            _cameraProvider.Initialize(_mainCamera);
    }
}
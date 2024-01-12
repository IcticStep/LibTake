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
        
        public override void InstallBindings()
        {
            BindCamera();
            BindSelfAsInitializable();
        }

        public void Initialize() =>
            InitializeCameraProvider();

        private void BindCamera() =>
            Container
                .Bind<ICameraFollow>()
                .To<CameraFollow>()
                .FromInstance(_mainCamera)
                .AsSingle();

        private void BindSelfAsInitializable() =>
            Container.Bind<IInitializable>().To<LibraryInstaller>().FromInstance(this);

        private void InitializeCameraProvider() =>
            Container
                .Resolve<ICameraProvider>()
                .Initialize(_mainCamera);
    }
}